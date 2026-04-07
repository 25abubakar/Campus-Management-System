using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PersonController : Controller
{
    private readonly AppDbContext _context;

    public PersonController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult persons()
    {
        ViewBag.Courses = _context.Courses.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult OnlineCheckup(PersonModel person, int[] StudentCourseIds, int[] TeacherCourseIds)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Courses = _context.Courses.ToList();
            TempData["Error"] = "Please correct the errors in the form.";
            return View(person);
        }

        using var transaction = _context.Database.BeginTransaction();
        try
        {

            _context.Persons.Add(person);
            _context.SaveChanges();

            if (StudentCourseIds != null)
            {
                foreach (var cid in StudentCourseIds)
                {
                    _context.StudentCourse.Add(new StudentCourse
                    {
                        PersonId = PersonModel.Person.PersonID,
                        CourseId = cid
                    });
                }
            }

            if (TeacherCourseIds != null)
            {
                foreach (var cid in TeacherCourseIds)
                {
                    _context.TeacherCourse.Add(new TeacherCourse
                    {
                        PersonId = person.PersonId,
                        CourseId = cid
                    });
                }
            }

            _context.SaveChanges();
            transaction.Commit();
            TempData["Success"] = "Person added and courses assigned successfully!";
            return RedirectToAction("Index");
        }
        catch (DbUpdateException ex)
        {
            transaction.Rollback();
            TempData["Error"] = "Database error: " + ex.Message;
            ViewBag.Courses = _context.Courses.ToList();
            return View(person);
        }
    }

    public IActionResult Index()
    {
        var persons = _context.Persons
            .Include(p => p.StudentCourse)
                .ThenInclude(sc => sc.Course)
            .Include(p => p.TeacherCourse)
                .ThenInclude(tc => tc.Course)
            .ToList();

        return View(persons);
    }
}