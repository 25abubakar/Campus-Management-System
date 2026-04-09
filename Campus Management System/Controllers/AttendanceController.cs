using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        // LIST PAGE
        public IActionResult Index()
        {
            var data = _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Course)
                .ToList();

            return View(data);
        }

        // CREATE PAGE
        public IActionResult Create()
        {
            ViewBag.Students = _context.Students
                .Include(s => s.Person)
                .ToList();

            ViewBag.Courses = _context.Courses.ToList();

            return View();
        }

        // SAVE ATTENDANCE
        [HttpPost]
        public IActionResult Create(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}