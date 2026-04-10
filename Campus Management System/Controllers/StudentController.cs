using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            StudentCreateViewModel vm = new StudentCreateViewModel();

            vm.StudentPersons = _context.Persons
                .Where(p => p.Role == "Student")
                .ToList();

            vm.Courses = _context.Courses.ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.StudentPersons = _context.Persons
                    .Where(p => p.Role == "Student")
                    .ToList();

                vm.Courses = _context.Courses.ToList();
                return View("Index", vm);
            }

            var existingStudent = _context.Students
                .FirstOrDefault(s => s.PersonId == vm.Student.PersonId);

            if (existingStudent == null)
            {
                _context.Students.Add(vm.Student);
                _context.SaveChanges();
                existingStudent = vm.Student;
            }

            if (vm.SelectedCourseIds != null)
            {
                foreach (var courseId in vm.SelectedCourseIds)
                {
                    bool alreadyAssigned = _context.StudentCourses
                        .Any(x => x.StudentId == existingStudent.StudentId && x.CourseId == courseId);

                    if (!alreadyAssigned)
                    {
                        StudentCourse sc = new StudentCourse()
                        {
                            StudentId = existingStudent.StudentId,
                            CourseId = courseId
                        };

                        _context.StudentCourses.Add(sc);
                    }
                }

                _context.SaveChanges();
            }

            TempData["Success"] = "🔥Assign Course To Student Saved Successfully!";
            return RedirectToAction("Index");
        }
    }
}