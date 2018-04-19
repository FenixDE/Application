using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Semester()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Student()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Teacher()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Admin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}