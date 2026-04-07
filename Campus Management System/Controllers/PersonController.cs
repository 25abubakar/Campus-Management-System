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

    public IActionResult Persons()
    {
        ViewBag.Courses = _context.Courses.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult SavePerson(
        PersonModel person,
        StudentModel student,
        TeacherModel teacher,
        int[] StudentCourseIds,
        int[] TeacherCourseIds)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View("Persons", person);
        }

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            // 🔹 Save Person
            _context.Persons.Add(person);
            _context.SaveChanges();

            // 🔹 Student Flow
            if (person.Role == "Student")
            {
                student.PersonId = person.PersonId;

                _context.Students.Add(student);
                _context.SaveChanges();

                if (StudentCourseIds != null)
                {
                    foreach (var cid in StudentCourseIds)
                    {
                        _context.StudentCourses.Add(new StudentCourse
                        {
                            StudentId = student.StudentId,
                            CourseId = cid
                        });
                    }
                }
            }

            // 🔹 Teacher Flow
            else if (person.Role == "Teacher")
            {
                teacher.PersonId = person.PersonId;

                _context.Teachers.Add(teacher);
                _context.SaveChanges();

                if (TeacherCourseIds != null)
                {
                    foreach (var cid in TeacherCourseIds)
                    {
                        _context.TeacherCourses.Add(new TeacherCourse
                        {
                            TeacherId = teacher.TeacherId,
                            CourseId = cid
                        });
                    }
                }
            }

            _context.SaveChanges();
            transaction.Commit();

            TempData["Success"] = "Saved successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            TempData["Error"] = ex.Message;

            ViewBag.Courses = _context.Courses.ToList();
            return View("Persons", person);
        }
    }

    public IActionResult Index()
    {
        var persons = _context.Persons
            .Include(p => p.Student)
                .ThenInclude(s => s.StudentCourse)
                    .ThenInclude(sc => sc.Course)
            .Include(p => p.Teacher)
                .ThenInclude(t => t.TeacherCourse)
                    .ThenInclude(tc => tc.Course)
            .ToList();

        return View(persons);
    }
}