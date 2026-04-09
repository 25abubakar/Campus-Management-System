using Microsoft.AspNetCore.Mvc;
using Campus_Management_System.Data;

namespace Campus_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalTeachers = _context.Teachers.Count();
            ViewBag.TotalCourses = _context.Courses.Count();

            var today = DateTime.Today;

            var todayAttendance = _context.Attendances
                .Where(a => a.Date.Date == today);

            int totalMarked = todayAttendance.Count();
            int presentCount = todayAttendance.Count(a => a.IsPresent);

            ViewBag.TodayAttendancePercent = totalMarked == 0
                ? 0
                : (presentCount * 100) / totalMarked;

            return View();
        }
    }
}