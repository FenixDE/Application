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
    public class GroupController : Controller
    {
        // GET: Group
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.groups = await Models.Group.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Group group)
        {
            bool result = await group.Push();
            //if(result)
                return Redirect(Request.UrlReferrer.ToString());
            //else
            //    return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> AddToSemester(string id, string semesterid)
        {
            Group group = await Models.Group.GetInstanceAsync(id);
            if(group == null)
                return View("~/Views/Shared/Error.cshtml");

            Semester semester = await Semester.GetInstanceAsync(semesterid);
            if(semester == null)
                return View("~/Views/Shared/Error.cshtml");

            bool result = await group.AddToSemester(id, semesterid);
            return Redirect(String.Format("/Group/Group/{0}",id));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteToSemester(string id, string semesterid)
        {
            Group group = await Models.Group.GetInstanceAsync(id);
            if(group == null)
                return View("~/Views/Shared/Error.cshtml");

            Semester semester = await Semester.GetInstanceAsync(semesterid);
            if(semester == null)
                return View("~/Views/Shared/Error.cshtml");

            if(group.DeleteToSemester(id, semesterid))
                return Redirect(String.Format("/Group/Group/{0}", id));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Group group = await Models.Group.GetInstanceAsync(ID);
            group.ID = ID;
            if (group.Delete())
                return Redirect(Request.UrlReferrer.ToString());
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpPost] //
        public async Task<ActionResult> Up(Group group)
        {
            if(await group.Update())
                return Redirect("/Faculty");
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Group group = await Models.Group.GetInstanceAsync(ID);
            if (group == null)
                return View("~/Views/Shared/Error.cshtml");

            ViewBag.group = group; //запись полей
            return View();
        }

        public async Task<ActionResult> Group(string ID)
        {
            if (ID != null)
            {
                Group group = await Models.Group.GetInstanceAsync(ID);
                ViewBag.group = group;
                List<Semester> semesters = await Semester.GetCollectionAsync();
                semesters = semesters.Except(group.Semesters).ToList();
                ViewBag.semesters = semesters;
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}