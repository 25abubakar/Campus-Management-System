using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Persons()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View(new PersonEntryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePerson(PersonEntryViewModel model)
        {
            if (model == null || model.Person == null)
            {
                TempData["Error"] = "Data binding failed.";
                return RedirectToAction("Persons");
            }

            if (model.Person.Role == "Student")
            {
                ModelState.Remove("Teacher");
            }
            else
            {
                ModelState.Remove("Student");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _context.Courses.ToList();
                return View("Persons", model);
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Persons.Add(model.Person);
                _context.SaveChanges();

                if (model.Person.Role == "Student")
                {
                    model.Student!.PersonId = model.Person.PersonId;
                    _context.Students.Add(model.Student);
                    _context.SaveChanges();

                    if (model.SelectedCourseIds != null)
                    {
                        foreach (var cid in model.SelectedCourseIds)
                        {
                            _context.StudentCourses.Add(new StudentCourse { StudentId = model.Student.StudentId, CourseId = cid });
                        }
                    }
                }
                else if (model.Person.Role == "Teacher" || model.Person.Role == "SeniorTeacher")
                {
                    model.Teacher!.PersonId = model.Person.PersonId;
                    _context.Teachers.Add(model.Teacher);
                    _context.SaveChanges();

                    if (model.SelectedCourseIds != null)
                    {
                        foreach (var cid in model.SelectedCourseIds)
                        {
                            _context.TeacherCourses.Add(new TeacherCourse { TeacherId = model.Teacher.TeacherId, CourseId = cid });
                        }
                    }
                }

                _context.SaveChanges();
                transaction.Commit();
                TempData["Success"] = "Data saved successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                TempData["Error"] = "System Error: " + ex.Message;
                ViewBag.Courses = _context.Courses.ToList();
                return View("Persons", model);
            }
        }

        public IActionResult Index()
        {
            var persons = _context.Persons
                .Include(p => p.Students!)
                    .ThenInclude(s => s.StudentCourse!)
                        .ThenInclude(sc => sc.Course)
                .Include(p => p.Teachers!)
                    .ThenInclude(t => t.TeacherCourse!)
                        .ThenInclude(tc => tc.Course)
                .ToList();

            ViewBag.Appointments = persons;
            return View(persons);
        }
    }
}