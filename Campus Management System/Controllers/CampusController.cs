using Campus_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CampusController : Controller
{
    private readonly AppDbContext _context;

    public CampusController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Get latest 5 students
        var recentStudents = _context.Students
            .Include(s => s.Person)
            .OrderByDescending(s => s.StudentId)
            .Take(5)
            .ToList();
     
        ViewBag.RecentStudents = recentStudents;

        // Get latest 5 Teachers
        var recentTeachers = _context.Teachers
            .Include(t => t.Person)
            .OrderByDescending(t => t.TeacherId)
            .Take(5)
            .ToList();

        ViewBag.RecentTeachers = recentTeachers ;
        return View();
    }
}