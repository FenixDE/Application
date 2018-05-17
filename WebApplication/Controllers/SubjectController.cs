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
            if (result)
                return Redirect("/Subject");
            else
                return View("~/Views/Shared/Error.cshtml");
        }


        [HttpPost]
        public async Task<ActionResult> Add(Subject subject)
        {
            bool result = await subject.Push();
            if (result)
                return Redirect("/Department");
            else
                return View("~/Views/Shared/Error.cshtml");
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
            if (await subject.Update())
                return Redirect("/Subject");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpGet]
        public async Task<ActionResult> Up(string ID)
        {
            Subject subject = await Models.Subject.GetInstanceAsync(ID);
            if (subject == null)
                return View("~/Views/Shared/Error.cshtml");
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
        
        public async Task<ActionResult> FlowSubjectM(string id)
        {
            if (id != null)
            {
                FlowSubject subject = await FlowSubject.GetInstanceAsync(id);
                ViewBag.subject = subject;

                return View("FlowSubject");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}