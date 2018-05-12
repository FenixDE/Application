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
    public class StudentController : Controller
    {
        // GET: Student
        //public async Task<ActionResult> Index()
        //{
        //    string fileURL = ConfigurationManager.AppSettings["RequestPath"];
        //    ViewBag.students = await Student.GetCollectionAsync();
        //    return View();
        //}
        [HttpPost]
        public async Task<ActionResult> Add(Student student)
        {
            await student.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Student student = new Student();
            student.ID = ID;
            if (student?.Delete() ?? false)
                return Redirect("/Student");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
    }
}