using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

public class PersonController : Controller
{
    private readonly AppDbContext _context;

    public PersonController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnlineCheckup()
    {
        ViewBag.Courses = _context.Courses.ToList();
        ViewBag.Persons = _context.Persons.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult OnlineCheckup(PersonModel person, int[] CourseIds)
    {
        if (ModelState.IsValid)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();

            foreach (var cid in CourseIds)
            {
                _context.PersonCourses.Add(new StudentCourse
                {
                    PersonId = person.PersonId,
                    CourseId = cid
                });
            }

            _context.SaveChanges();
            TempData["Success"] = "Person Added Successfully!";
        }

        return RedirectToAction("Index");
    }
}