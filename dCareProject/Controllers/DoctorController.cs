using dCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCareProject.Controllers {
    public class DoctorController : Controller {
        dCareEntities db = new dCareEntities();

        // GET: Doctor
        public ActionResult Index() {
            var query = (from c in db.預約表
                         join o in db.病人 on c.病人ID equals o.ID


                         //醫生ID:10 魏宏和
                         where c.醫生ID == 10 && c.登記時間 >= DateTime.Today
                         select new listWaitView {
                             name = o.姓名,
                             id = c.ID,
                             time = c.登記時間.Value,
                             address = o.地址,
                             gender = o.性別,
                             phone = o.電話,
                             pid = o.ID,
                             birth = o.出生年月日,
                             linkid = c.看診紀錄表ID.Value,
                             checkin = c.報到結果
                         }).ToList();

            ViewBag.name = query;
            Session["patName"] = query[0].name;

            return View();
        }

        public ActionResult Login() {
            return View();
        }

        public ActionResult Link(int id) {
            var temp = id;
            var query = (from p in db.看診紀錄表
                         join o in db.預約表 on p.ID equals o.看診紀錄表ID
                         join c in db.病人 on o.病人ID equals c.ID
                         where p.ID == temp
                         select new linkdata {
                             id = p.ID,
                             pid = o.病人ID.Value,
                             name = c.姓名,
                             weight = p.體重.Value,
                             temperature = p.體溫.Value,
                             pulse = p.脈搏.Value,
                             blood = p.血氧.Value,
                             time = p.健檢時間,
                             record = p.看診紀錄,
                         }).ToList().Last();

            ViewBag.data = query;
            return View();
        }

        [HttpPost]
        public ActionResult Link(int id, 看診紀錄表 viewdata, 預約表 data) {
            看診紀錄表 dbData = db.看診紀錄表.Find(id);
            dbData.看診紀錄 = viewdata.看診紀錄;

            var a = new 預約表();
            a.病人ID = 3;
            a.醫生ID = 10;
            a.登記時間 = data.登記時間;
            db.預約表.Add(a);

            db.SaveChanges();
            return RedirectToAction("Index","Doctor");
        }

        public ActionResult History() {
            var query = from o in db.預約表
                        join c in db.病人 on o.病人ID equals c.ID
                        where o.醫生ID == 10 &&
                        o.登記時間 < DateTime.Today
                        select new listWaitView {
                            name = c.姓名,
                            id = o.ID,
                            pid = c.ID
                        };

            ViewBag.name = query;
            return View();
        }
    }
}