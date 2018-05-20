using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using WebApplication.Providers;

namespace WebApplication.Controllers
{
    public class StudentController : Controller
    {
        //GET: Student
        [HttpGet]
        [Route("Student/{groupId}/{semesterId}")]
        public async Task<ActionResult> Index(string groupId, string semesterId)
        {
            //string fileURL = ConfigurationManager.AppSettings["RequestPath"];
            var students = await Student.GetCollectionAsync(groupId, semesterId);
            if(students == null)
                return View("~/Views/Shared/Error.cshtml");

            var group = await Group.GetInstanceAsync(groupId);
            if(group == null)
                return View("~/Views/Shared/Error.cshtml");

            ViewBag.students = students;
            ViewBag.group = group;
            ViewBag.semesterId = semesterId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(Student student)
        {
            if(await student.Push())
                return Redirect(string.Format("/Student/{0}/{1}", student.GroupId, student.SemesterId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<ActionResult> Add(Student.StudentFlowSubject registration)
        {
            if(registration != null)
            {
                if (await registration.Add())
                    return View(); //TODO: редирект на страницу списка зареганных студентов
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Route("Student/Del/{ID}")]
        public async Task<ActionResult> DelStudent(string ID)
        {
            //TODO: необходимо получить студента по id
            Student student = await Student.GetInstanceAsync(ID);
            if (student?.Delete() ?? false)
                return Redirect(string.Format("/Student/{0}/{1}", student.GroupId, student.SemesterId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Route("Student/Del/registry/{ID}")]
        public async Task<ActionResult> DelRegistration(string ID)
        {
            Student.StudentFlowSubject registry = new Student.StudentFlowSubject
            {
                ID = ID
            };
            if (await registry.Delete())
                return Redirect(Request.UrlReferrer.ToString());
            else
                return View("~/Views/Shared/Error.cshtml");
        }
        //
        //[HttpGet]
        //[Route("Student/{flowSubjectId}/registry")]
        //[AllowCrossSiteJsonAttribute]
        //public async Task<ActionResult> GetStudentRegistry(string flowSubjectId)
        //{
        //    var students = await Student.StudentFlowSubject.GetCollectionAsync(flowSubjectId);
        //    ViewBag.students = students;
        //    ViewBag.flowSubjectId = flowSubjectId;
        //    //'Access-Control-Allow-Origin'
            
        //    return View("Registration");
        //}
        //
        //[HttpGet]
        //[Route("Student/{flowSubjectId}/labwork")]
        //[AllowCrossSiteJsonAttribute]
        //public async Task<ActionResult> GetStudentLab(string flowSubjectId)
        //{
        //    var students = await Student.StudentFlowSubject.GetCollectionAsync(flowSubjectId);
        //    ViewBag.students = students;
        //    ViewBag.flowSubjectId = flowSubjectId;
        //    //'Access-Control-Allow-Origin'
        //    return View("LabWorkStudents");
        //}

        [HttpGet]
        [Route("Student/{StudentId}")]
        [AllowCrossSiteJsonAttribute]
        public async Task<ActionResult> GetSubjectStudent(string StudentId)
        {
            var students = await Student.StudentFlowSubject.GetCollectionAsync(null, StudentId);
            ViewBag.students = students; //регистрация на предмет
            ViewBag.StudentId = StudentId;
            return View("StudentSubject");
        }

        [HttpGet]
        [Route("Student/registry/{flowSubjectId}")]
        public async Task<ActionResult> AddRegistration(string flowSubjectId)
        {
            List<Semester> semesters = await Semester.GetCollectionAsync();
            ViewBag.semesters = semesters;
            List<Group> groups = await Group.GetCollectionAsync();
            ViewBag.groups = groups;
            ViewBag.flowSubjectId = flowSubjectId;
            return View("AddReg");
        }
        
        [HttpPost]
        [Route("Student/registry")]
        public async Task<ActionResult> AddRegistration(string groupId, string semesterId, 
            string studentId, string flowSubjectId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                List<Student> students = await Student.GetCollectionAsync(groupId, semesterId);
                List<Person> people = await Person.GetCollectionAsync();
                ViewBag.students = students;
                ViewBag.flowSubjectId = flowSubjectId;
                return View("AddReg");
            }
            else
            {
                Student.StudentFlowSubject studentFlowSubject = new Student.StudentFlowSubject
                {
                    StudentId = studentId,
                    FlowSubjectId = flowSubjectId
                };
                if (await studentFlowSubject.Add())
                    return Redirect(string.Format("/Student/{0}/registry", flowSubjectId));
                else
                    return View("~/Views/Shared/Error.cshtml");
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> LabWorkStudents(string flowSubjectId)
        //{
        //    var fss = await FlowSubject.GetInstanceAsync(flowSubjectId);
        //    ViewBag.fss = fss;
        //    var lp = await Lab.GetInstanceAsync(flowSubjectId);
        //    ViewBag.lp = lp;
        //    return View("LabWorkStudents");
        //}
    }
}