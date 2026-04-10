using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
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