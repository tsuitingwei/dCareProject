using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCareProject.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(string username ,string password)
        //{
        //    return RedirectToAction("Index","Doctor");
        //}
    }
}