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
    public class PersonController : Controller
    {
        // GET: Person
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.people = await Models.Person.GetCollectionAsync();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Person person)
        {
            bool result = await person.Push();
            if (result)
                return Redirect("Index");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Person person = new Person();
            person.ID = ID;
            if (person?.Delete() ?? false)
                return Redirect("/Person");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Person person)
        {
            if (await person.Update())
                return Redirect("/Person");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpGet]
        public async Task<ActionResult> Up(string ID)
        {
            Person person = await Models.Person.GetInstanceAsync(ID);
            if (person == null)
                return View("~/Views/Shared/Error.cshtml");
            person.ID = ID;
            ViewBag.person = person; //запись полей
            return View("Up");
        }
        public async Task<ActionResult> Person(string ID)
        {
            if (ID != null)
            {
                Person person = await Models.Person.GetInstanceAsync(ID);
                ViewBag.person = person;
                //var departments = await Department.GetCollectionAsync();
                //ViewBag.departments = departments;
                //var students = await Student.GetCollectionAsync(ID,gId);
                //students = students.FindAll(x => x.PersonId == person.ID);
                //ViewBag.students = students;
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}