using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quantum.Domain.Abstractions;
using Quantum.Domain.Entities;
using Quantum.Repository;

namespace Quantum.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IRepository _repository;

        #endregion


        #region Constructor

        // TODO: Real world project, i would use dependency injection
        public HomeController()
        {
            _repository = new Repository.Repository();
        }

        #endregion

        //
        // GET: /Home/

        public ActionResult Index()
        {
            var classes = _repository.GetAllClasses();
            return View(classes);
        }

        public ActionResult CreateClass()
        {
            
            return View();
        }

        public ActionResult EditClass(int classId)
        {
            var @class = _repository.GetClass(classId);
            return View(@class);
        }

        [HttpPost]
        public ActionResult EditClass(Class updatedClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateClass(updatedClass);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(updatedClass);
        }

        [HttpPost]
        public ActionResult CreateClass(Class model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateClass(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        public ActionResult CreateStudent(int classId)
        {
            var student = new Student() {ClassId = classId};
            return View(student);
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingClass = _repository.GetClass(student.ClassId);
                    var allStudents = _repository.GetAllStudents();

                    existingClass.AddStudent(student, allStudents);

                    _repository.SaveClass(existingClass);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(student);
        }

        public ActionResult EditStudent(int studentid)
        {
            var student = _repository.GetStudent(studentid);
            return View(student);
        }

        [HttpPost]
        public ActionResult EditStudent(Student updatedStudent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateStudent(updatedStudent);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(updatedStudent);
        }

        public ActionResult DeleteClass(int classId)
        {
            _repository.DeleteClass(classId);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteStudent(int studentId)
        {
            _repository.DeleteStudent(studentId);
            return RedirectToAction("Index");
        }

        public ActionResult ClassDetail(int classId)
        {
            var theClass = _repository.GetClass(classId);
            return PartialView("ClassDetail", theClass);
        }
    }
}
