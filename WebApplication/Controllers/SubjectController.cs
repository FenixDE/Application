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
    public class SubjectController : Controller
    {
        // GET: Subject
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.subjects = await Models.Subject.GetCollectionAsync();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddToFlow(FlowSubject flowSubject)
        {
            bool result = await Models.Subject.AddToFlow(flowSubject);
            return Redirect("/Subject");
        }
        

        [HttpPost]
        public async Task<ActionResult> Add(Subject subject)
        {
            bool result = await subject.Push();
            return Redirect("/Department");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Subject subject = new Subject();
            subject.ID = ID;
            if (subject?.Delete() ?? false)
                return Redirect("/Department");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Subject subject)
        {
            await subject.Update();
            return Redirect("/Subject");
        }
        [HttpGet] 
        public async Task<ActionResult> Up(string ID)
        {
            Subject subject = await Models.Subject.GetInstanceAsync(ID);
            subject.ID = ID;
            ViewBag.subject = subject; //запись полей
            return View("Up");
        }
        public async Task<ActionResult> Subject(string ID)
        {
            if (ID != null)
            {
                Subject subject = await Models.Subject.GetInstanceAsync(ID);
                ViewBag.subject = subject;                
                List<Flow> flows = await Flow.GetCollectionAsync();
                ViewBag.flows = flows;
                List<Person> people = await Person.GetCollectionAsync();
                ViewBag.people = people;
                List<FlowSubject> sFlows = await subject.GetForFlow();
                ViewBag.sFlows = sFlows;
                List<Semester> semesters = await Semester.GetCollectionAsync();
                ViewBag.semesters = semesters;

                
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}