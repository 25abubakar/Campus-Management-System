using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Campus_Management_System.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        // SHOW PAGE
        public IActionResult Index()
        {
            TeacherCreateViewModel vm = new TeacherCreateViewModel();

            vm.TeacherPersons = _context.Persons
                .Where(p => p.Role == "Teacher" || p.Role == "SeniorTeacher")
                .ToList();

            vm.Courses = _context.Courses.ToList();

            return View(vm);
        }

        // SAVE TEACHER PROFILE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeacherCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TeacherPersons = _context.Persons
                    .Where(p => p.Role == "Teacher" || p.Role == "SeniorTeacher")
                    .ToList();

                vm.Courses = _context.Courses.ToList();
                return View("Index", vm);
            }

            _context.Teachers.Add(vm.Teacher);
            _context.SaveChanges();

            // MANY TO MANY
            if (vm.SelectedCourseIds != null)
            {
                foreach (var courseId in vm.SelectedCourseIds)
                {
                    TeacherCourse tc = new TeacherCourse()
                    {
                        TeacherId = vm.Teacher.TeacherId,
                        CourseId = courseId
                    };

                    _context.TeacherCourses.Add(tc);
                }

                _context.SaveChanges();
            }

            TempData["Success"] = "Teacher Created Successfully!";
            return RedirectToAction("Index");
        }
    }
}