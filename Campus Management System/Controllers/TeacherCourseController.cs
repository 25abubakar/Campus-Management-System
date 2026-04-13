using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class TeacherCourseController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherCourseController(AppDbContext context)
        {
            _context = context;
        }

        // LOAD INDEX
        public IActionResult Index()
        {
            return View(LoadIndexWithAssignForm(null));
        }

        private TeacherCourseIndexVM LoadIndexWithAssignForm(TeacherCourseAssignVM assignVm)
        {
            TeacherCourseIndexVM vm = new TeacherCourseIndexVM();

            vm.Enrollments = _context.TeacherCourses
                .Include(tc => tc.Teacher)
                    .ThenInclude(t => t.Person)
                .Include(tc => tc.Course)
                .ToList();

            vm.AssignVM = assignVm;

            return vm;
        }

        // OPEN ASSIGN FORM BELOW TABLE
        public IActionResult Assign(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.Person)
                .FirstOrDefault(t => t.TeacherId == id);

            TeacherCourseAssignVM vm = new TeacherCourseAssignVM
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.Person.Name,
                Qualification = teacher.Qualification,
                Salary = teacher.Salary,

                Courses = _context.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    }).ToList()
            };

            return View(vm);
        }

        // SAVE ASSIGNED COURSES
        [HttpPost]
        public IActionResult Assign(TeacherCourseAssignVM vm)
        {
            foreach (var courseId in vm.SelectedCourses)
            {
                bool exists = _context.TeacherCourses
                    .Any(tc => tc.TeacherId == vm.TeacherId && tc.CourseId == courseId);

                if (!exists)
                {
                    _context.TeacherCourses.Add(new TeacherCourse
                    {
                        TeacherId = vm.TeacherId,
                        CourseId = courseId
                    });
                }
            }

            _context.SaveChanges();
            TempData["Success"] = "Courses Assigned Successfully!";
            return RedirectToAction("Index");
        }

        // EDIT COURSE
        public IActionResult Edit(int teacherId, int courseId)
        {
            var enrollment = _context.TeacherCourses
                .Include(tc => tc.Teacher)
                .ThenInclude(t => t.Person)
                .FirstOrDefault(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);

            TeacherCourseEditVM vm = new TeacherCourseEditVM
            {
                TeacherId = teacherId,
                CourseId = courseId,
                OldCourseId = courseId,
                TeacherName = enrollment.Teacher.Person.Name,

                Courses = _context.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TeacherCourseEditVM vm)
        {
            var old = _context.TeacherCourses
                .FirstOrDefault(tc => tc.TeacherId == vm.TeacherId && tc.CourseId == vm.OldCourseId);

            _context.TeacherCourses.Remove(old);
            _context.SaveChanges();

            _context.TeacherCourses.Add(new TeacherCourse
            {
                TeacherId = vm.TeacherId,
                CourseId = vm.CourseId
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //DELETE
        public IActionResult Delete(int teacherId, int courseId)
        {
            var enrollment = _context.TeacherCourses.Find(teacherId, courseId);
            _context.TeacherCourses.Remove(enrollment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}