using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace WebApplication.Controllers
{
    public class LabController : Controller
    {
        // GET: Lab
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.labs = await Models.Lab.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Lab lab, string id)
        {
            bool result = await lab.Push(id);
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
            Lab lab = await Models.Lab.GetInstanceAsync(ID);
            if (lab == null)
                ViewBag.lab = lab; //запись полей
            return View();
        }

        [Route("Lab/plan/{fsid}")]
        public async Task<ActionResult> LabPlan(string fsid)
        {
            var subflow = await FlowSubject.GetInstanceAsync(fsid);
            ViewBag.subflow = subflow;
            var workplan = await LabWorkPlan.GetCollectionAsync(fsid);
            ViewBag.workplan = workplan;
            var works = await Models.Lab.GetCollectionAsync();
            ViewBag.works = works;
            return View("PlanLab");
        }

        [HttpPost]
        [Route("Lab/plan")]
        public async Task<ActionResult> AddLabPlan(LabWorkPlan plan)
        {
            if (plan == null)
                return View("~/Views/Shared/Error.cshtml");

            if (await plan.Push())
                return Redirect(string.Format("/Lab/plan/{0}", plan.FlowSubjectId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        //Удалить лаб раб из плана
        [HttpPost]
        [Route("Lab/plan/{id}")]
        public async Task<ActionResult> DeleteLabPlan(string id)
        {
            LabWorkPlan plan = new LabWorkPlan
            {
                ID = id
            };
            if (plan.DeleteOfPlan())
                return Redirect(Request.UrlReferrer.ToString());
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        // получить список выполненных студентом лаб работ по предмету (все рег. пользователи)
        [HttpGet]
        [Route("Lab/exec/{studentFlowId}/{subjectFlowId}")]
        public async Task<ActionResult> GetExec(string studentFlowId, string subjectFlowId)
        {
            
            FlowSubject fSubject = await FlowSubject.GetInstanceAsync(subjectFlowId);
            var exLabs = await ExecutedLabWork.GetExec(studentFlowId);
            var workPlan = await LabWorkPlan.GetCollectionAsync(subjectFlowId);

            if (fSubject == null)
                return View("~/Views/Shared/Error.cshtml");

            ViewBag.exLabs = exLabs;
            ViewBag.fSubject = fSubject;
            ViewBag.workPlan = workPlan;
            ViewBag.studentFlowId = studentFlowId;
            return View("LabWorkStudent");
        }

        // установить лаб работу из плана как выполненную студентом (преподаватель, администратор)
        [HttpPost]
        [Route("Lab/exec")]
        public async Task<ActionResult> PostExec(ExecutedLabWork exLab)
        {
            if(exLab == null)
                return View("~/Views/Shared/Error.cshtml");

            if (await exLab.AddExec())
                return Redirect(Request.UrlReferrer.ToString());
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        public async Task<ActionResult> Lab(string ID)
        {
            if (ID != null)
            {
                Lab lab = await Models.Lab.GetInstanceAsync(ID);
                ViewBag.lab = lab;               
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}