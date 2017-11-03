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

            var query = (from c in db.預約表
                         join o in db.病人 on c.病人ID equals o.ID

                         where (
                         c.醫生ID == 10 &&
                         c.登記時間 >= DateTime.Today
                         )

                         select new listWaitView
                         {
                             name = o.姓名,
                             id = c.ID,
                             time = c.登記時間.Value,
                             address = o.地址,
                             gender = o.性別,
                             phone = o.電話,
                             pid = o.ID,
                             birth = o.出生年月日
                         }).ToList();

            ViewBag.name = query;

            return View();
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            return RedirectToAction("Link", "Doctor");
        }



        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Link()
        {
            var query = (from p in db.看診紀錄表
                         join c in db.病人 on p.ID equals c.ID
                         join o in db.預約表 on p.ID equals o.看診紀錄表ID
                         where o.登記時間 >= new DateTime()

                         select new linkdata
                         {
                             id = p.ID,
                             pid = c.ID,
                             name = c.姓名,
                             weight = p.體重.Value,
                             temperature = p.體溫.Value,
                             pulse = p.脈搏.Value,
                             blood = p.血氧.Value,
                             time = p.健檢時間,
                             record = p.看診紀錄
                         }).First();

            ViewBag.data = query;

            return View();
        }

        [HttpPost]
        public ActionResult Link(看診紀錄表 data)
        {
            預約表 c = new 預約表();
                c.病人ID = 3;
                c.醫生ID = 10;
                c.登記時間 = new DateTime();
                c.預約結果 = "預約成功";
                db.預約表.Add(c);

            db.看診紀錄表.Add(data);
            db.SaveChanges();

            return RedirectToAction ( "Index", "Doctor");
        }

        public ActionResult History()
        {

            var query = from o in db.預約表
                        join c in db.病人 on o.病人ID equals c.ID
                        where o.醫生ID == 10 &&
                        o.登記時間 < DateTime.Today
                        select new listWaitView
                        {
                            name = c.姓名,
                            id = o.ID,
                            pid = c.ID
                            
                        };


            ViewBag.name = query;
            return View();

        }

    }
}