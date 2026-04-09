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
        //    public IActionResult Delete(int id)
        //    {
        //        var teacherCourse = _context.TeacherCourses.Find(id);
        //        if (teacherCourse == null)
        //            return NotFound();

        //        _context.TeacherCourses.Remove(teacherCourse);
        //        _context.SaveChanges();
        //        TempData["Success"] = "Teacher deleted successfully!";
        //        return RedirectToAction("Index");
        //    }
    }
}