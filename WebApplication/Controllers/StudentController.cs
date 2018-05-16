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
        //GET: Student
        [HttpGet]
        [Route("Student/{groupId}/{semesterId}")]
        public async Task<ActionResult> Index(string groupId, string semesterId)
        {
            //string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            var students = await Student.GetCollectionAsync(groupId, semesterId);
            if(students == null)
                return View("~/Views/Shared/Error.cshtml");

            var group = await Group.GetInstanceAsync(groupId);
            if(group == null)
                return View("~/Views/Shared/Error.cshtml");

            ViewBag.students = students;
            ViewBag.group = group;
            ViewBag.semesterId = semesterId;
            return View();
        }

        [Route("Test")]
        public async Task<ActionResult> my()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Add(Student student)
        {
            if(await student.Push())
                return Redirect(string.Format("/Student/{0}/{1}", student.GroupId, student.SemesterId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            //TODO: необходимо получить студента по id
            Student student = new Student();
            student.ID = ID;
            if (student?.Delete() ?? false)
                return Redirect("/Student");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
    }
}