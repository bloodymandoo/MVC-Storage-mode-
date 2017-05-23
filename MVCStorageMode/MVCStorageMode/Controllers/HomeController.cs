using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStorageMode.DAL;
using MVCStorageMode.Models;

namespace MVCStorageMode.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private XEngineContext db = new XEngineContext();
        public ActionResult Index()
        {
            var user = db.SysRoles.Count();
            return View(user);
        }
	}
}