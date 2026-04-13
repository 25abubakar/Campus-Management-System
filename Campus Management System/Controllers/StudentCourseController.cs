using Campus_Management_System.Data;
using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly AppDbContext _context;

        public StudentCourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var enrollments = _context.StudentCourses
                .Include(tc => tc.Student)
                    .ThenInclude(t => t.Person)
                .Include(tc => tc.Course)
                .ToList();

            return View(enrollments);
        }

        // EDIT (open form)
        public IActionResult Edit(int studentId, int courseId)
        {
            var enrollment = _context.StudentCourses
                .Include(sc => sc.Student)
                    .ThenInclude(s => s.Person)
                .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (enrollment == null)
                return NotFound();

            StudentCourseEditVM vm = new StudentCourseEditVM();

            vm.StudentId = studentId;
            vm.CourseId = courseId;
            vm.OldCourseId = courseId; 
            vm.StudentName = enrollment.Student.Person.Name;

            vm.Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.CourseName
                }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(StudentCourseEditVM vm)
        {
            var oldEnrollment = _context.StudentCourses
                .FirstOrDefault(sc => sc.StudentId == vm.StudentId
                                   && sc.CourseId == vm.OldCourseId);

            if (oldEnrollment == null)
                return NotFound();

            _context.StudentCourses.Remove(oldEnrollment);
            _context.SaveChanges();

            StudentCourse newEnrollment = new StudentCourse
            {
                StudentId = vm.StudentId,
                CourseId = vm.CourseId
            };

            _context.StudentCourses.Add(newEnrollment);
            _context.SaveChanges();

            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction("Index");
        }

        // Delete
        public IActionResult Delete(int studentId, int courseId)
        {
            var enrollment = _context.StudentCourses.Find(studentId, courseId);

            if (enrollment == null)
                return NotFound();

            _context.StudentCourses.Remove(enrollment);
            _context.SaveChanges();

            TempData["Success"] = "Course Assignment Removed successfully!";
            return RedirectToAction("Index");
        }
    }
}