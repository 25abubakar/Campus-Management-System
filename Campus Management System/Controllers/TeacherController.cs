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

        public IActionResult Index()
        {
            TeacherCreateViewModel vm = new TeacherCreateViewModel();

            vm.TeacherPersons = _context.Persons
                .Where(p => p.Role == "Teacher" || p.Role == "SeniorTeacher")
                .ToList();

            vm.Courses = _context.Courses.ToList();

            return View(vm);
        }

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

            var existingTeacher = _context.Teachers
                .FirstOrDefault(t => t.PersonId == vm.Teacher.PersonId);

            if (existingTeacher == null)
            {
                _context.Teachers.Add(vm.Teacher);
                _context.SaveChanges();
                existingTeacher = vm.Teacher;
            }

            if (vm.SelectedCourseIds != null)
            {
                foreach (var courseId in vm.SelectedCourseIds)
                {
                    bool alreadyAssigned = _context.TeacherCourses
                        .Any(x => x.TeacherId == existingTeacher.TeacherId && x.CourseId == courseId);

                    if (!alreadyAssigned)
                    {
                        TeacherCourse tc = new TeacherCourse()
                        {
                            TeacherId = existingTeacher.TeacherId,
                            CourseId = courseId
                        };

                        _context.TeacherCourses.Add(tc);
                    }
                }

                _context.SaveChanges();
            }

            TempData["Success"] = "🔥 Assighn Course To Teacher Successfully!";
            return RedirectToAction("Index");
        }
    }
}