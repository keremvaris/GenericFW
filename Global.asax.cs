using GenericFW.DataAccessLayer;
using GenericFW.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GenericFW
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Remove(typeof(byte[]));
            ModelBinders.Binders.Add(typeof(byte[]), new UploadModelBinder());
            ModelBinders.Binders.DefaultBinder = new CustomModelBinder();

            //using (var repo = new Repository<User>())
            //{
            //    var user = repo.FindById(6);
            //    user.Name += "?";
            //    repo.Update(user);
            //};

            //using (var db = new DataAccessLayer.DatabaseContext())
            //{
            //	db.Users.ToList().ForEach(c => db.Users.Remove(c));
            //	db.SaveChanges();
            //}
        }
	}
}
