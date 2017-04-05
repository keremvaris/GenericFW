using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenericFW.Attr;
using System.Reflection;
using PagedList;
using System.Dynamic;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using das = System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Data;
using GenericFW.Areas.Yonetim.GenericVM;
using static GenericFW.Areas.Yonetim.GenericVM.CrudExtensions;
using GenericFW.Areas.Yonetim.Controllers;

namespace GenericFW.Areas.Yonetim.JsonManager
{
    public class JSONValue
    {
        public string column { get; set; }
        public object value { get; set; }

    }

    public class JSONEntry
    {
        public JSONEntry()
        {
            values = new List<JSONValue>();
        }
        public List<JSONValue> values { get; set; }

    }

    public class JSONData
    {
        public JSONData()
        {
            data = new List<JSONEntry>();
        }
        public List<JSONEntry> data { get; set; }
        public int? page { get; set; }
        public int? pagecount { get; set; }

    }
}