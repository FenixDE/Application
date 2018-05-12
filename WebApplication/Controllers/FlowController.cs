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
    public class FlowController : Controller
    {
        // GET: Flow
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.flows = await Models.Flow.GetCollectionAsync();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Flow flow)
        {
            await flow.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Flow flow = new Flow();
            flow.ID = ID;
            if (flow?.Delete() ?? false)
                return Redirect("/Flow");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Flow flow)
        {
            await flow.Update();
            return Redirect("/Flow");
        }
        [HttpGet]
        public async Task<ActionResult> Up(string ID)
        {
            Flow flow = await Models.Flow.GetInstanceAsync(ID);
            flow.ID = ID;
            ViewBag.flow = flow; //запись полей
            return View("Up");
        }
        public async Task<ActionResult> Flow(string ID)
        {
            if (ID != null)
            {
                Flow flow = await Models.Flow.GetInstanceAsync(ID);
                ViewBag.flow = flow;
                var departments = await Department.GetCollectionAsync();
                ViewBag.departments = departments;
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }        
    }
}