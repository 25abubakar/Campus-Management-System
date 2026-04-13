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

        public IActionResult Index()
        {
            var enrollments = _context.TeacherCourses
                .Include(tc => tc.Teacher)
                    .ThenInclude(t => t.Person)
                .Include(tc => tc.Course)
                .ToList();

            return View(enrollments);
        }

        // EDIT (open form)
        public IActionResult Edit(int teacherId, int courseId)
        {
            var enrollment = _context.TeacherCourses
                .Include(tc => tc.Teacher)
                .ThenInclude(t => t.Person)
                .FirstOrDefault(tc => tc.TeacherId == teacherId && tc.CourseId == courseId);

            if (enrollment == null)
                return NotFound();

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
            var oldEnrollment = _context.TeacherCourses
                .FirstOrDefault(tc => tc.TeacherId == vm.TeacherId
                                   && tc.CourseId == vm.OldCourseId);

            if (oldEnrollment == null)
                return NotFound();

            _context.TeacherCourses.Remove(oldEnrollment);
            _context.SaveChanges();

            TeacherCourse newEnrollment = new TeacherCourse
            {
                TeacherId = vm.TeacherId,
                CourseId = vm.CourseId
            };

            _context.TeacherCourses.Add(newEnrollment);
            _context.SaveChanges();

            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction("Index");
        }

        // Delete
        public IActionResult Delete(int teacherId, int courseId)
        {
            var enrollment = _context.TeacherCourses.Find(teacherId, courseId);

            if (enrollment == null)
                return NotFound();

            _context.TeacherCourses.Remove(enrollment);
            _context.SaveChanges();

            TempData["Success"] = "Course Assignment Removed successfully!";
            return RedirectToAction("Index");
        }
    }
}