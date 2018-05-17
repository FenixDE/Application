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
            bool result = await department.Push();
            if (result)
                return Redirect("Index");
            else
                return View("~/Views/Shared/Error.cshtml");
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
            bool res = await department.Update();
            if (res)
                return Redirect("/Department");
            else
                return View("~/Views/Shared/Error.cshtml");
            
        }
        [HttpGet] //по ID cnhfybwf c ajhv
        public async Task<ActionResult> Up(string ID)
        {
            Department department = await Models.Department.GetInstanceAsync(ID);
            if (department == null)
                return View("~/Views/Shared/Error.cshtml");
            department.ID = ID;
            ViewBag.department = department; //запись полей
            return View();
        }
        public async Task<ActionResult> Department(string ID)
        {
            if (ID != null)
            {
                Department department = await Models.Department.GetInstanceAsync(ID);
            ViewBag.department = department;
            var subjects = await Subject.GetCollectionAsync();
            subjects = subjects.FindAll(x => x.DepartmentID == department.ID);
            ViewBag.subjects = subjects;
            return View("Look");
            }
            else return View("~/Views/Shared/Error.cshtml");
        }
    }
}