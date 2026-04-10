using Campus_Management_System.Data;
using Campus_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
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