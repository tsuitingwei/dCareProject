using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dCareProject.Models;

namespace dCareProject.Controllers {
    public class PatientController : Controller {
        private dCareEntities db = new dCareEntities();
        // GET: Patient
        public ActionResult Index() {
            return View();
        }

        public ActionResult step1() {
            return View();
        }
        public ActionResult step2() {
            return View();
        }
        public ActionResult step4() {
            return View();
        }

        public ActionResult step3() {
            var randon = (from o in db.醫生
                         orderby Guid.NewGuid()
                         select o).Take(1);

            return View(randon.ToList());
        }
        public ActionResult look() {
            var query = from o in db.預約表
                        join p in db.醫生 on o.醫生ID equals p.ID
                        where o.病人ID == 8
                        select new patient {
                            section = p.科別,
                            date = o.登記時間.Value,
                            doctor = p.姓名
                                                    };
                        ViewBag.name = query.ToList();
                        return View();
        }
        
         public ActionResult look1() {
            var query = from o in db.醫生


                         select o;

                   return View(query.ToList());
        }

        public ActionResult patientview() {
            return View();
        }
        public ActionResult patientreservation() {
            return View();
        }
    }
}

    

