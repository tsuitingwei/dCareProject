using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dCareProject.Models;

namespace dCareProject.Controllers
{
    public class PatientloginController : Controller
    {
        private dCareEntities db = new dCareEntities();
        // GET: Patientlogin
        public ActionResult login()
        {
            return View();
        }
        public ActionResult patientreservation() {
            var query = from o in db.預約表
                        join p in db.醫生 on o.醫生ID equals p.ID
                        join c in db.病人 on o.病人ID equals c.ID
                        join d in db.看診紀錄表 on o.看診紀錄表ID equals d.ID
                        where c.ID == 3

                        select new patient {
                            section = p.科別,
                            date = o.登記時間.Value,
                            doctor = p.姓名,
                            name = c.姓名,
                            gender = c.性別,
                            number = c.電話,
                            address = c.地址,
                            birthday = c.出生年月日,
                            addressID = c.看診點ID.Value,
                            healthDate = d.健檢時間,
                            patientid = o.ID
                            
                           

                        };
            ViewBag.name = query.ToList();
 
            return View();
            
    }
        public ActionResult look1() {
            var query = from o in db.醫生


                        select o;
            //System.Threading.Thread.Sleep(3000);
            //return RedirectToAction("patientreservation", "Patientlogin");
            return View(query.ToList());
        }
    }
}