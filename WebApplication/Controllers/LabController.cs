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
    public class LabController : Controller
    {
        // GET: Lab
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.labs = await Lab.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Lab lab)
        {
            bool result = await lab.Push();
            if (result)
                return Redirect("/Index");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Lab lab = new Lab();
            lab.ID = ID;
            if (lab?.Delete() ?? false)
                return Redirect("/Lab");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Lab lab)
        {
            if (await lab.Update())
                return Redirect("/Lab");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Up(string ID)
        {
            Lab lab = await Lab.GetInstanceAsync(ID);
            if (lab == null)
                ViewBag.lab = lab; //запись полей
            return View();
        }

        [Route("Lab/plan/{fsid}")]
        public async Task<ActionResult> LabPlan(string fsid)
        {
            var subflow = await FlowSubject.GetInstanceAsync(fsid);
            ViewBag.subflow = subflow;
            var workplan = await Models.LabPlan.GetCollectionAsync(fsid);
            ViewBag.workplan = workplan;
            var works = await Lab.GetCollectionAsync();
            ViewBag.works = works;
            return View("PlanLab");
        }

        [HttpPost]
        [Route("Lab/plan")]
        public async Task<ActionResult> AddLabPlan(LabPlan plan)
        {
            if (plan == null)
                return View("~/Views/Shared/Error.cshtml");

            if (await plan.Push())
                return Redirect(string.Format("/Lab/plan/{0}", plan.FlowSubjectId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Get(string fsid)
        {
            var subflow = await FlowSubject.GetInstanceAsync(fsid);
            ViewBag.subflow = subflow;
            //var sts = await Student.GetCollectionAsync();
            //ViewBag.sts = await Lab.GetM(subflow);
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Ad(Lab lab)
        {
            bool result = await lab.AddM();
            if (result)
                return Redirect("/Index");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> D(string ID)
        {
            Lab lab = new Lab();
            lab.ID = ID;
            if (lab?.DelM() ?? false)
                return Redirect("/Lab");
            else
                return View("~/Views/Shared/Error.cshtml");
        }        
    }
}