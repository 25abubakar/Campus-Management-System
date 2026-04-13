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

    // LIST PAGE
    public IActionResult Index()
    {
        CourseEntryViewModel vm = new CourseEntryViewModel();
        vm.CoursesList = _context.Courses.ToList();
        return View(vm);
    }

    // CREATE
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

    // OPEN EDIT PAGE
    public IActionResult Edit(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
            return NotFound();

        return View(course);
    }

    // SAVE EDIT
    [HttpPost]
    public IActionResult Edit(Course course)
    {
        if (!ModelState.IsValid)
            return View(course);

        _context.Courses.Update(course);
        _context.SaveChanges();

        TempData["Success"] = "Course updated successfully!";
        return RedirectToAction("Index");
    }

    // DELETE
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null)
            return NotFound();

        _context.Courses.Remove(course);
        _context.SaveChanges();

        TempData["Success"] = "Course deleted successfully!";
        return RedirectToAction("Index");
    }
}