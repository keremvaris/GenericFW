using GenericFW.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenericFW.Areas.Yonetim.Controllers
{
	public class UserController : GenericController<User>
	{
        //İsteyen Crud Kullanmayacağım, özel işler yapacağım ben diyen burdan devam etsin.
        //Tüm Crud ActionResultları Virtual Tanımlandı.   Override edilebilir.
        //Örnek:
		//public override ActionResult Edit(User entity)
		//{
		//	var result = base.Edit(entity);
		//	//using (var rep = new DataAccessLayer.Repository<User>())
		//	//{

		//	//}
		//	return result;
		//}

	}
}