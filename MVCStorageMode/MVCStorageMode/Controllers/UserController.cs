using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStorageMode.Repositories;
using MVCStorageMode.Models;
using MVCStorageMode.DAL;

namespace MVCStorageMode.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        //private ISysUserRepository userRepository = new SysUserRepository(new DAL.XEngineContext());
        //private IGenericRepository<SysUser> userRepository = new GenericRepository<SysUser>(new XEngineContext());
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            //var users = unitOfWork.SysUserRepository.Get();
            var users = unitOfWork.SysUserRepository.Get(orderBy: q => q.OrderBy(u => u.Name));
            return View(users);
        }
        
        public ActionResult Delete(int id)
        {
            unitOfWork.SysUserRepository.Delete(id);
            unitOfWork.Save();
            unitOfWork.Dispose();        
            return RedirectToAction("Index");
        }
	}
}