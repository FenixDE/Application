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
            ViewBag.labs = await Models.Lab.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Lab lab)
        {
            await lab.Push();
            return Redirect("Index");
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
            await lab.Update();
            return Redirect("/Lab");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Lab lab = await Models.Lab.GetInstanceAsync(ID);
            ViewBag.lab = lab; //запись полей
            return View();
        }
        public async Task<ActionResult> Lab(string fsid)
        {
            Lab lab = await Models.Lab.GetCollectionAsync();
            ViewBag.lab = lab;
            var subflow = await FlowSubject.GetInstanceAsync(fsid);
            ViewBag.subflow = subflow;
            return View("Look");
        }
    }
}