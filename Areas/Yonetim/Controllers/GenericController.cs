using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GenericFW.Areas.Yonetim.GenericVM;
using GenericFW.Areas.Yonetim.JsonManager;
using System.Reflection;
using System.Linq.Dynamic;
using GenericFW.DataAccessLayer;
using GenericFW.Entities;
using System.Data.Entity.Core.Objects;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace GenericFW.Areas.Yonetim.Controllers
{
    public class GenericController<E> : Controller where E : EntityBase, new()
    {
        public GenericController()
        {
            Db = Activator.CreateInstance<Repository<E>>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Repository<E> Db { get; set; }

        int PageSize = 10;

        const int TOTAL_ROW_COUNT = 10000;

        [HttpGet]
        public virtual ActionResult List(int? activePage = 1)
        {
            var cn = typeof(E).Name;


            Db.ListQueryable().Skip((activePage.Value - 1) * PageSize).Take(PageSize);

            //var data = ORM.Select("ORDER BY "+ cn +"ID DESC", activePage.Value, PageSize, null, SelectType.Where);
            var data = Db.ListQueryable().OrderBy("Id DESC")
                    .Skip((activePage.Value - 1) * PageSize).Take(PageSize).ToList();

            var list = data.ToListVM();

            list.Page = new PageVM { ActivePage = activePage.Value, TotalRowCount = (int)(TOTAL_ROW_COUNT - 1) / (int)Math.Ceiling((double)PageSize) + 1,
                SearchVM = typeof(E).ToVM(new E()) };

            return View(list);
        }

        [HttpPost]
        public virtual ActionResult List(E entity, int? activePage = 1)
        {

            var vm = entity.ToVM();
            var meta = vm.Meta;
            var result = vm.Data.ToList();

            var primaryKey = meta.PrimaryColumn;

            var query = Db.ListQueryable();

            foreach (var item in result)
            {
               
                if (item.Value != null && item.Key != primaryKey.Name && item.Key != "_type")
                {
                    var column = item.Key;
                    var value = item.Value;
                    var metaColumn = meta.Columns.First(c => c.Property.Name == column);

                    var p = typeof(E).GetProperties().FirstOrDefault(x => x.Name.ToLower() == column.ToLower());

                    // bool
                    if (p.PropertyType == typeof(bool))
                        continue;

                    // Enum
                    if (p.PropertyType.IsEnum && (int)value == 0)
                        continue;
                    // Lookup
                    if (metaColumn.PrimaryTable != null && (int)value == 0)
                        continue;

                    if (p.PropertyType == typeof(String))
                    {
                        query = query.Where($"{column}.StartsWith(@0)", value);
                    }
                    else if (p.PropertyType == typeof(DateTime))
                    {
                        var d = (DateTime)value;
                        if (d > DateTime.MinValue)
                            query = query.Where($"{column}>=@0 AND {column}<@1", d, d.AddDays(1));
                    }
                    else if (value != null)
                    {
                        query = query.Where($"{column}==@0", value);
                    }

                }

            }

            var data = query.ToList();


            var list = data.ToListVM();

            list.Page = new PageVM { ActivePage = activePage.Value, TotalRowCount = (int)(TOTAL_ROW_COUNT - 1) / (int)Math.Ceiling((double)PageSize) + 1,
                SearchVM = typeof(E).ToVM(new E())
            };

            return View(list);




        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            var o = Db.FindById(id).ToVM();

            return View(o);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {

            var data = Db.FindById(id).ToVM();

            return View(data);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            var empty = new E();

            var data = empty.ToVM();

            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Create(E entity)
        {
            if (Db.Insert(entity) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                TempData["Mesaj"] = "Kayıt yaratılamadı!";
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public virtual ActionResult Delete(E entity)
        {
            if (Db.DeleteById(entity.Id) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                TempData["Mesaj"] = "Kayıt Bulunamadı!";
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(E entity)
        {
            /*
            if(Request.Files.Count>0)
            {
                var file = Request.Files[0];
                var meta = CrudExtensions.Get(typeof(E));
                // Ilk byte[] alani bul
                var col = meta.Columns.FirstOrDefault(c => c.DataType.DataType == DataType.Upload);
                using(var ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    col.Property.SetValue(entity, ms.ToArray());
                }
            }
            */
            if (Db.Update(entity) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                TempData["Mesaj"] = "Güncellenemedi!";
                return RedirectToAction("Index", "Home");
            }

        }

        public JsonResult Select(int page = 1, int count = 50)
        {
            var data = Db.ListQueryable()
                    .Skip((page - 1) * PageSize).Take(PageSize).ToList();
            var j = ToJSON(data);
            j.page = page;
            j.pagecount = TOTAL_ROW_COUNT / count;

            return Json(j, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult Search(string column, string value)
        {
            var p = typeof(E).GetProperties().FirstOrDefault(x => x.Name.ToLower() == column.ToLower());
            if (p == null)
            {
                return null;
            }
            string where = "";

            if (p.PropertyType.Name.Contains("String"))
            {
                where = string.Format("WHERE {0} LIKE @0", column);
                value = $"%{value}%";

            }
            else
            {
                where = string.Format("WHERE {0}=@0", column);

            }
            var result = Db.ListQueryable().Where(where, value).ToList();

            return Json(ToJSON(result));
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
           
                try
                {

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

            
            return View();
        }

        JSONData ToJSON(List<E> data)
        {
            var props = typeof(E).GetProperties();
            JSONData result = new JSONData();
            if (data != null && data.Any())
            {
                foreach (var row in data)
                {
                    JSONEntry entry = new JSONEntry();
                    foreach (var col in props)
                    {
                        JSONValue val = new JSONValue();
                        val.column = col.Name;
                        val.value = col.GetValue(row);
                        entry.values.Add(val);
                    }
                    result.data.Add(entry);
                }
                return result;
            }
            else
            {
                return null;
            }


        }
    }

}