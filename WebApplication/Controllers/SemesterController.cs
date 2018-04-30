using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class SemesterController : Controller
    {
        // GET: Semester
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.semesters = await Models.Semester.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Semester semester)
        {
            await semester.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Semester semester = new Semester();
            semester.ID = ID;
            if (semester?.Delete() ?? false)
                return Redirect("/Semester");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Semester semester)
        {
            await semester.Update();
            return Redirect("/Index");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Semester semester = await Models.Semester.GetInstanceAsync(ID);
            semester.ID = ID;
            ViewBag.department = semester; //запись полей
            return View();
        }
        public async Task<ActionResult> Semester(string ID)
        {
            Semester semester = await Models.Semester.GetInstanceAsync(ID);
            ViewBag.semester = semester;
            return View("Look");
        }
    }
}