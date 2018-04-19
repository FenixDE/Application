using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faculty()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
