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
    public class DepartmentController : Controller
    {
        // GET: Department
        public async Task<ActionResult> Index()
        {
            string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            ViewBag.departments = await Models.Department.GetCollectionAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Department department)
        {
            await department.Push();
            return Redirect("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Del(string ID)
        {
            Department department = new Department();
            department.ID = ID;
            if (department?.Delete() ?? false)
                return Redirect("/Department");
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        [HttpPost] //
        public async Task<ActionResult> Up(Department department)
        {
            await department.Update();
            return Redirect("/Index");
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Department department = await Models.Department.GetInstanceAsync(ID);
            department.ID = ID;
            ViewBag.department = department; //запись полей
            return View();
        }
        public async Task<ActionResult> Department(string ID)
        {
            Department department = await Models.Department.GetInstanceAsync(ID);
            ViewBag.department = department;
            return View("Look");
        }
    }
}