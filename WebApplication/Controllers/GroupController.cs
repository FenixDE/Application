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
            return Redirect("/Faculty");
        }
        [HttpPost]
        public async Task<ActionResult> AddToSemester(string id, string semesterid)
        {
            Group group = await Models.Group.GetInstanceAsync(id);
            group.ID = id;
            ViewBag.group = group;
            Semester semester = await Semester.GetInstanceAsync(semesterid);
            semester.ID = semesterid;
            ViewBag.semester = semester;
            bool result = await group.AddToSemester(id, semesterid);
            return Redirect(String.Format("/Group/Group/{0}",id));
        }
        public async Task<ActionResult> DeleteToSemester(string id, string semesterid)
        {
            Group group = await Models.Group.GetInstanceAsync(id);
            group.ID = id;
            ViewBag.group = group;
            Semester semester = await Semester.GetInstanceAsync(semesterid);
            semester.ID = semesterid;
            ViewBag.semester = semester;
            group.DeleteToSemester(id, semesterid);
            return Redirect(String.Format("/Group/Group/{0}", id));
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Group group = new Group();
            group.ID = ID;
            if (group?.Delete() ?? false)
                return Redirect("/Faculty");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Group group)
        {
            await group.Update();
            return Redirect("/Faculty");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Group group = await Models.Group.GetInstanceAsync(ID);
            if (ID != null)
            {
                group.ID = ID;
                ViewBag.group = group; //запись полей
                return View();
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
        public async Task<ActionResult> Group(string ID, string gId)
        {
            if (ID != null)
            {
                Group group = await Models.Group.GetInstanceAsync(ID);
                ViewBag.group = group;
                List<Semester> semesters = await Semester.GetCollectionAsync();
                ViewBag.semesters = semesters;
                //var students = await Student.GetCollectionAsync(ID, gId);
                //students = students.FindAll(x => x.GroupId == group.ID);
                //ViewBag.students = students;
                return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}