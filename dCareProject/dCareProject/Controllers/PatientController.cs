using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dCareProject.Models;

namespace dCareProject.Controllers {
    public class PatientController : Controller {
        DBManager dbm = new DBManager();
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
        [HttpGet]
        public ActionResult step4() {
            var viewData = new 預約表();
            return View(viewData);

            
        }
        [HttpPost]
        public ActionResult step4(int ID,預約表 viewData) {

            預約表 dbData = db.預約表.Find(ID);

            dbData.報到結果 = viewData.報到結果;

            db.SaveChanges();

            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("index", "Patient");
            //return View();
          
        }

        [HttpGet]
        public ActionResult step3() {
            
            var randon = (from o in db.醫生

                          where o.ID != 10 && o.科別 == "家醫科"
                          orderby Guid.NewGuid()

                          select o).Take(1);

            return View(randon.ToList());
        }

        [HttpPost]
        public ActionResult step3(int ID, 預約表 viewData) {

            預約表 dbData = db.預約表.Find(ID);

            dbData.報到結果 = viewData.報到結果;
            dbData.醫生ID = viewData.醫生ID;

            db.SaveChanges();


            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("index", "Patient");
        }




        public ActionResult look() {
            var query = from o in db.預約表
                        join p in db.醫生 on o.醫生ID equals p.ID
                        join c in db.看診紀錄表 on o.看診紀錄表ID equals c.ID
                        where o.病人ID == 3
                        select new patient {
                            section = p.科別,
                            date = o.登記時間.Value,
                            doctor = p.姓名,
                            healhdate = c.健檢時間,
                            patientid = o.ID
                                                    };
                        ViewBag.name = query.ToList();
                        return View();
        }
        
         public ActionResult look1() {
            var query = from o in db.醫生
                         
                         select o;
            //醫生 viewData = query.SingleOrDefault();
            //return View(viewData);
            return View(query.ToList());
        }
        //[HttpPost]
        //public ActionResult look1(int id) {


        //    System.Threading.Thread.Sleep(3000);
        //    return RedirectToAction("patientreservation", "Patient");
        //    //return View();
        //}

        public ActionResult patientview() {
            return View();
        }
        public ActionResult patientreservation() {
            return View();
        }
    }
}

    

