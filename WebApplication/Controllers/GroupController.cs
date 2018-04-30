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
            await group.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Group group = new Group();
            group.ID = ID;
            if (group?.Delete() ?? false)
                return Redirect("/Group");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Group group)
        {
            await group.Update();
            return Redirect("/Index");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            //Faculty faculty = new Faculty();
            //subjects = subjects.FindAll(x => x.DepartmentID == department); делегат
            //faculty.ID = ID;
            //Faculty.GetInstanceAsync(ID);
            Group group = await Models.Department.GetInstanceAsync(ID);
            group.ID = ID;
            ViewBag.department = group; //запись полей
            return View();
        }
        public async Task<ActionResult> Group(string ID)
        {
            Group group = await Models.Group.GetInstanceAsync(ID);
            ViewBag.group = group;
            return View("Look");
        }
    }
}