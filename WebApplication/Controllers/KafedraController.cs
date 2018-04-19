using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class KafedraController : Controller
    {
        // GET: Kafedra
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kaf()
        {
            return View();
        }
    }
}