using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Recruitment()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Facilities()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}