using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dCareProject.Models;

namespace dCareProject.Controllers
{
    public class seeadoctorController : Controller
    {
        private dCareEntities db = new dCareEntities();
        // GET: seeadoctor
        public ActionResult patientview()
        {
            var query = (from o in db.預約表
                        join p in db.醫生 on o.醫生ID equals p.ID
                        where o.看診結果 =="候診"
                        select new patient {
                            section = p.科別,
                            doctor = p.姓名,
                            daddress = p.診所地址,
                            daid =p.診所名稱,
                            docid =p.ID,
                            img=p.圖片
                        }).Take(1);
            ViewBag.name = query.ToList();


            return View();
        }

        public ActionResult login() {

            return View();
        }
    }
}