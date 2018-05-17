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
    public class FacultyController : Controller
    {
        // GET: Faculty
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.faculties = await Models.Faculty.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Faculty faculty)
        {
            await faculty.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Faculty faculty = new Faculty();
            faculty.ID = ID;
            if (faculty?.Delete() ?? false)
                return Redirect("/Faculty");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Faculty faculty)
        { 
            await faculty.Update();
            return Redirect("/Faculty");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Faculty faculty = await Models.Faculty.GetInstanceAsync(ID);
            ViewBag.faculty = faculty; //запись полей
            return View();
        }
        public async Task<ActionResult> Faculty(string ID)
        {
            Faculty faculty = await Models.Faculty.GetInstanceAsync(ID);
            ViewBag.faculty = faculty;
            var groups = await Group.GetCollectionAsync();
            groups = groups.FindAll(x => x.FacultyId == faculty.ID);
            ViewBag.groups = groups;
            return View("Look");
        }        
    }
}
