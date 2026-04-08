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

    // SHOW PAGE
    public IActionResult Index()
    {
        CourseEntryViewModel vm = new CourseEntryViewModel();
        vm.CoursesList = _context.Courses.ToList();
        return View(vm);
    }

    // SAVE COURSE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CourseEntryViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.CoursesList = _context.Courses.ToList();
            return View("Index", vm);
        }

        _context.Courses.Add(vm.Course);
        _context.SaveChanges();

        TempData["Success"] = "Course Added Successfully!";
        return RedirectToAction("Index");
    }
}