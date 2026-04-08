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
                .Include(sc => sc.Student)
                    .ThenInclude(s => s.Person)
                .Include(sc => sc.Course)
                .ToList();

            return View(enrollments);
        }
    }
}