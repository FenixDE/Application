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
            var departments = await Department.GetCollectionAsync();
            ViewBag.departments = departments;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Flow flow)
        {
            bool res = await flow.Push();
            if(res)
                return Redirect("Index");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Flow flow = await Models.Flow.GetInstanceAsync(ID);
            if(flow == null)
                return View("~/Views/Shared/Error.cshtml");

            if (flow.Delete())
                return Redirect("/Flow");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost] //
        public async Task<ActionResult> Up(Flow flow)
        {
            bool res = await flow.Update();
            if(res)
                return Redirect("/Flow");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Up(string ID)
        {
            Flow flow = await Models.Flow.GetInstanceAsync(ID);
            if(flow == null)
                return View("~/Views/Shared/Error.cshtml");

            ViewBag.flow = flow; //запись полей
            return View("Up");
        }

        public async Task<ActionResult> Flow(string ID)
        {
            if (ID != null)
            {
                Flow flow = await Models.Flow.GetInstanceAsync(ID);
                if(flow == null)
                    return View("~/Views/Shared/Error.cshtml");

                ViewBag.flow = flow;
                
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }        
    }
}