﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication.Models;
using System.Web.Mvc;
using System.Threading.Tasks;


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
        public async Task<ActionResult> Del(string ID)
        {
            //TODO: необходимо получить студента по id
            Student student = await Student.GetInstanceAsync(ID);
            if (student?.Delete() ?? false)
                return Redirect(string.Format("/Student/{0}/{1}", student.GroupId, student.SemesterId));
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [HttpGet]
        [Route("Student/{flowSubjectId}/registry")]
        public async Task<ActionResult> GetStudentRegistry(string flowSubjectId)
        {
            var studentsRegistry = await Student.StudentFlowSubject.GetCollectionAsync(flowSubjectId);
            List<Student> students = new List<Student>(studentsRegistry.Count);
            foreach(var studentReg in studentsRegistry)
                students.Add(await Student.GetInstanceAsync(studentReg.StudentId));

            ViewBag.students = students;
            return View();
        }
    }
}