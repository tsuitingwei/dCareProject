using dCareProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dCareProject.Controllers
{
    public class AdminController : Controller
    {

        dCareEntities db = new dCareEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Doctor()
        {
            var query = from o in db.醫生
                        select o;

            List<醫生> view = query.ToList();
            return View(view);
        }

        public ActionResult Patient()
        {
            var query = from o in db.病人
                        select o;

            List<病人> view = query.ToList();
            return View(view);
        }

        public ActionResult All()
        {
            return View();
        }


    }
}