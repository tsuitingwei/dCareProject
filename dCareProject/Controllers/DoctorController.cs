using dCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCareProject.Controllers
{
    public class DoctorController : Controller
    {
        dCareEntities db = new dCareEntities();
       
        // GET: Doctor
        public ActionResult Index()
        {
            var linquery = from c in db.預約表.AsEnumerable()
                         join o in db.病人.AsEnumerable() on c.病人ID equals o.ID
                         where c.醫生ID == 10
                         select new listWaitView{
                             time = c.登記時間.Value,
                             name = o.姓名,
                             id = c.ID
                         };


            //List<string> q = query.Select(s => String.Format("{0}", s)).ToList();

           

            ViewBag.name = linquery.ToList();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Link()
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