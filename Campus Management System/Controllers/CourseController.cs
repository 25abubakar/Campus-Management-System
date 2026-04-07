using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

public class CourseController : Controller
{
    private readonly AppDbContext _context;
    public CourseController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Courses.ToList());
    }

    [HttpPost]
    public IActionResult Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}