using Campus_Management_System.Data;
using Campus_Management_System.Models;
using CampusManagementSystem.Models;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePerson(
            Person person,
            Student student,
            Teacher teacher,
            int[] StudentCourseIds,
            int[] TeacherCourseIds)
        {
            {
                if (person.Role == "Student")
                {
                    ModelState.ClearValidationState(nameof(teacher));
                    ModelState.MarkFieldValid(nameof(teacher));
                }
                else if (person.Role == "Teacher" || person.Role == "SeniorTeacher")
                {
                    ModelState.ClearValidationState(nameof(student));
                    ModelState.MarkFieldValid(nameof(student));
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var err in errors) { Console.WriteLine(err.ErrorMessage); }

                    ViewBag.Courses = _context.Courses.ToList();
                    return View("Persons", person);
                }

                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    _context.Persons.Add(person);
                    _context.SaveChanges();

                    if (person.Role == "Student")
                    {
                        _context.Students.Add(student);
                        _context.SaveChanges();

                        if (StudentCourseIds != null)
                        {
                            foreach (var cid in StudentCourseIds)
                            {
                                _context.StudentCourses.Add(new StudentCourse
                                {
                                    StudentId = student.StudentId,
                                    CourseId = cid
                                });
                            }
                        }
                    }
                    else if (person.Role == "Teacher" || person.Role == "SeniorTeacher")
                    {
                        _context.Teachers.Add(teacher);
                        _context.SaveChanges();

                        if (TeacherCourseIds != null)
                        {
                            foreach (var cid in TeacherCourseIds)
                            {
                                _context.TeacherCourses.Add(new TeacherCourse
                                {
                                    TeacherId = teacher.TeacherId,
                                    CourseId = cid
                                });
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
                    TempData["Error"] = "Error: " + ex.Message;

                    ViewBag.Courses = _context.Courses.ToList();
                    return View("Persons", person);
                }
            }
        }

        public IActionResult Index()
        {
            var persons = _context.Persons
                .Include(p => p.Students)
                    .ThenInclude(s => s.StudentCourse)
                        .ThenInclude(sc => sc.Course)
                .Include(p => p.Teachers)
                    .ThenInclude(t => t.TeacherCourse)
                        .ThenInclude(tc => tc.Course)
                .ToList();

            ViewBag.Appointments = persons;

            return View(persons);
        }
    }
}