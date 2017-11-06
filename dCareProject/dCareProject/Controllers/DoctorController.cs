using dCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCareProject.Controllers {
    public class DoctorController : Controller {
        dCareEntities db = new dCareEntities();

        //class get
        //{
        //    public string Name { get; set; }
        //    public int ID { get; set; }
        //}

        // GET: Doctor
        public ActionResult Index() {
            var query = (from c in db.預約表
                         join o in db.病人 on c.病人ID equals o.ID
                         where c.醫生ID == 10
                         select new listWaitView {
                             name = o.姓名,
                             id = c.ID,
                             time = c.登記時間.Value,
                             address = o.地址,
                             gender = o.性別,
                             phone = o.電話,
                             pid = o.ID,
                             birth = o.出生年月日

                         }).ToList();

            var query2 = from o in db.病人
                         select o;

            ViewBag.name = query;
            ViewBag.two = query2;

            return View();
        }

        public ActionResult Login() {
            return View();
        }


        public ActionResult Link() {


            return View();
        }


    }
}