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

        public IActionResult Index(int? courseId, DateTime? date)
        {
            var query = _context.Attendances
                .Include(a => a.Student).ThenInclude(s => s.Person)
                .Include(a => a.Course)
                .AsQueryable();

            if (courseId.HasValue)
                query = query.Where(a => a.CourseId == courseId);

            if (date.HasValue)
                query = query.Where(a => a.Date.Date == date.Value.Date);

            ViewBag.Courses = _context.Courses.ToList();

            return View(query.ToList());
        }

        public IActionResult TakeAttendance()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult LoadStudents(int CourseId, DateTime Date)
        {
            bool alreadyMarked = _context.Attendances
                .Any(a => a.CourseId == CourseId && a.Date.Date == Date.Date);

            if (alreadyMarked)
            {
                TempData["Error"] = "Attendance already marked for this date!";
                return RedirectToAction("Index");
            }

            var students = _context.StudentCourses
                .Include(sc => sc.Student).ThenInclude(s => s.Person)
                .Where(sc => sc.CourseId == CourseId)
                .Select(sc => new StudentAttendanceRow
                {
                    StudentId = sc.StudentId,
                    StudentName = sc.Student.Person.Name
                })
                .ToList();

            AttendanceVM vm = new AttendanceVM
            {
                CourseId = CourseId,
                Date = Date,
                Students = students
            };

            return View("MarkAttendance", vm);
        }

        [HttpPost]
        public IActionResult SaveAttendance(AttendanceVM vm)
        {
            var allStudents = _context.StudentCourses
                .Where(sc => sc.CourseId == vm.CourseId)
                .Select(sc => sc.StudentId)
                .ToList();

            foreach (var studentId in allStudents)
            {
                bool isPresent = vm.PresentStudentIds.Contains(studentId);

                Attendance a = new Attendance()
                {
                    StudentId = studentId,
                    CourseId = vm.CourseId,
                    Date = vm.Date,
                    IsPresent = isPresent
                };

                _context.Attendances.Add(a);
            }

            _context.SaveChanges();

            TempData["Success"] = "Attendance saved successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var att = _context.Attendances.Find(id);

            if (att != null)
            {
                _context.Attendances.Remove(att);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}