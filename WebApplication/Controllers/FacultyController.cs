using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using ElJournal.Models;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.faculties = await Faculty.GetCollectionAsync();
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
            faculty.Delete();
            return Redirect("/Faculty");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Faculty faculty)
        { 
            await faculty.Update();
            return Redirect("/Index");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            //Faculty faculty = new Faculty();
            //subjects = subjects.FindAll(x => x.DepartmentID == department); делегат
            //faculty.ID = ID;
            //Faculty.GetInstanceAsync(ID);
            Faculty faculty = await Faculty.GetInstanceAsync(ID);
            faculty.ID = ID;
            ViewBag.faculty = faculty; //запись полей
            return View();
        }
        public async Task<ActionResult> Look(string ID)
        {
            Faculty faculty = new Faculty();
            faculty.ID = ID;
            await Faculty.GetInstanceAsync(ID);
            return Redirect("/Faculty");
        }
    }
}
