using Campus_Management_System.Data;
using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        // SHOW PAGE
        public IActionResult Index()
        {
            PersonEntryViewModel vm = new PersonEntryViewModel();

            vm.PersonsList = _context.Persons
                .Include(p => p.Students)
                .Include(p => p.Teachers)
                .ToList();

            return View(vm);
        }

        // SAVE PERSON
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePerson(PersonEntryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PersonsList = _context.Persons.ToList();
                return View("Index", vm);
            }

            try
            {
                _context.Persons.Add(vm.Person);
                _context.SaveChanges();

                TempData["Success"] = "Person Added Successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                vm.PersonsList = _context.Persons.ToList();
                return View("Index", vm);
            }
        }
    }
}