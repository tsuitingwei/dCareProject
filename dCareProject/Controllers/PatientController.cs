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
		
        public ActionResult HIDLogin() {
            //Process the JSON-string.
            jsonGather js = new jsonGather();
            var cid = js.getCardID;
            
            if (cid != "") {
                Session["CARD"] = cid;
                
                var card = Session["CARD"];
                var patName = (from o in db.病人
                               where o.卡號 == card.ToString()
                               select o.姓名).ToList();
                var patID = (from o in db.病人
                               where o.卡號 == card.ToString()
                               select o.ID).ToList();
                Session["NAME"] = patName[0];
                Session["ID"] = patID[0];

                DateTime st = DateTime.Now.AddDays(-1);
                DateTime ed = st.AddDays(2);
                int x = patID[0];

                var revQueryID = (from o in db.預約表
                               where o.登記時間 >= st && o.登記時間 <=ed && o.病人ID == x
                                select o.ID).ToList();
                Session["revID"] = revQueryID[0];
                return RedirectToAction("home");
            }

            var query = (from o in db.預約表
                        join p in db.病人 on o.病人ID equals p.ID
                        where o.登記時間 > DateTime.Today && o.報到結果 == "已報到"
                        select new patient{
                            ID = o.ID,
                            name = p.姓名 }
                        ).ToList();

            ViewBag.Board = query;


            return View();
        }

		
        public ActionResult home() {
            var query = (from o in db.預約表
                         join p in db.病人 on o.病人ID equals p.ID
                         where o.登記時間 > DateTime.Today && o.報到結果 == "已報到"
                         select new patient {
                             ID = o.ID,
                             name = p.姓名
                         }
            ).ToList();

            ViewBag.Board = query;

            return View();
        }


        // Measurment step : ViewResult.
        public ActionResult step1() {
            Session.Remove("WEIGHT");
            Session.Remove("BODYTEMP");
            Session.Remove("S2PO");
            Session.Remove("HEARTRATE");
            Session.Remove("START");

            return View();
        }

        // Measurment step : ViewResult.
        public ActionResult step1_start() {
            Session.Remove("WEIGHT");
            Session.Remove("BODYTEMP");
            Session.Remove("S2PO");
            Session.Remove("HEARTRATE");
            Session.Remove("START");

            return View();
        }

        // Measurment step : Measure Step.
        public ActionResult step1_request() {

            jsonGather jgather = new jsonGather();
            // For weight.
            if (string.IsNullOrEmpty(Session["WEIGHT"] as string) && !string.IsNullOrEmpty(Session["START"] as string)) {
                Session["WEIGHT"] = jgather.getWeight;
                return View();
            }

            //// For bodytemp.
            if (string.IsNullOrEmpty(Session["BODYTEMP"] as string) && !string.IsNullOrEmpty(Session["WEIGHT"] as string)) {
                Session["BODYTEMP"] = jgather.getTemp;
                return View();
            }

            ////For s2po
            if (string.IsNullOrEmpty(Session["S2PO"] as string) && !string.IsNullOrEmpty(Session["BODYTEMP"] as string)) {
                string s2po = jgather.getS2PO;
                var fs2po = float.Parse(s2po, System.Globalization.CultureInfo.InvariantCulture);
                if (fs2po > 80) {
                    Session["S2PO"] = s2po;
                }
                return View();
            }

            ////For heartrate
            if (string.IsNullOrEmpty(Session["HEARTRATE"] as string) && !string.IsNullOrEmpty(Session["S2PO"] as string)) {
                string hr = jgather.getHeartRate;
                var fhr = float.Parse(hr, System.Globalization.CultureInfo.InvariantCulture);
                if (fhr > 70) {
                    Session["HEARTRATE"] = hr;
                }

                return View();
            }


            return View();
        }



        public ActionResult step2() {
            var wt = Session["WEIGHT"].ToString();
            var bt = Session["BODYTEMP"].ToString();
            var s2po = Session["S2PO"].ToString();
            var ht = Session["HEARTRATE"].ToString();
            var rev = Session["revID"].ToString();

            //int wt = 60;
            //int ht = 80;
            //int rev = 55;
            //int s2po = 99;

            Single bt1 = Single.Parse(bt);
            int wt1 =  Convert.ToInt32(float.Parse(wt)/10);
            int s2po1 = Convert.ToInt32(float.Parse(s2po));
            int ht1 = Convert.ToInt32(float.Parse(ht));
            int rev1 = Convert.ToInt32(float.Parse(rev));


            var q = (from o in db.看診紀錄表
                     orderby o.ID descending
                     select o.ID).First();

            var addRecord = new 看診紀錄表 {
                體重 = wt1,
                體溫 = bt1,
                血氧 = s2po1,
                脈搏 = ht1,
                健檢時間 = DateTime.Now
            };

            db.看診紀錄表.Add(addRecord);
            db.SaveChanges();

            var w = (from o in db.預約表
                         where o.ID == rev1
                         select o).ToList();
            w[0].ID = w[0].ID;
            w[0].看診紀錄表ID = q + 1;

            db.SaveChanges();

            return View();
        }
        
        public ActionResult step4() {
            int rev = Convert.ToInt32(float.Parse(Session["revID"].ToString()));

            var w = (from o in db.預約表
                     where o.ID == rev
                     select o).ToList();
            w[0].ID = w[0].ID;
            w[0].報到結果 = "已報到";

            db.SaveChanges();

            var viewData = new 預約表();
            return View(viewData);
        }

        [HttpPost]
        public ActionResult step4(int ID,預約表 viewData) {

            預約表 dbData = db.預約表.Find(ID);

            dbData.報到結果 = viewData.報到結果;

            db.SaveChanges();

            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("HIDLogin", "Patient");
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

            Session.Remove("CARD");

            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("HIDLogin", "Patient");
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
                            patientid = o.ID,
                            temperture = c.體溫,
                            weigh =c.體重,
                            jump = c.脈搏,
                            spo2 =c.血氧
                        };
                        ViewBag.name = query.ToList();
                        return View();
        }
        
         public ActionResult look1(int id) {
            var query = from p in db.醫生
                        join o in db.預約表 on p.ID equals o.醫生ID
                        where o.ID==id
                           select new patient {
                            reservationid = o.ID,
                            date = o.登記時間.Value,
                            docid = p.ID,
                            section = p.科別,
                            docname =p.姓名
                        };
            ViewBag.name = query.ToList();
               return View();
        }
        [HttpPost]
        public ActionResult look1(int id, 預約表 viewData) {

            預約表 dbData = db.預約表.Find(id);

            var temp = dbData;


            dbData.醫生ID = viewData.醫生ID;
            dbData.登記時間 = viewData.登記時間;

            //System.Threading.Thread.Sleep(3000);
            db.SaveChanges();


            
            return RedirectToAction("look", "Patient");
        }
        

        public ActionResult patientview() {
            return View();
        }
        public ActionResult patientreservation() {
            return View();
        }
    }
}

    

