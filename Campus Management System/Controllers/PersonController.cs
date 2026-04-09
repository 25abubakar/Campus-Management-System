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

        public IActionResult Index()
        {
            PersonEntryViewModel vm = new PersonEntryViewModel();

            vm.PersonsList = _context.Persons
                .Include(p => p.Students)
                .Include(p => p.Teachers)
                .ToList();

            return View(vm);
        }

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

            // Edit Form
    public IActionResult Edit(int id)
        {
            var person = _context.Persons.Find(id);
            if (person == null)
                return NotFound();

            var vm = new PersonEntryViewModel
            {
                Person = person,
                PersonsList = _context.Persons.ToList()
            };
            return View("Index", vm);
        }

        // Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Persons.Update(person);
                _context.SaveChanges();
                TempData["Success"] = "Person updated successfully!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error updating person!";
            return RedirectToAction("Index");
        }

        // Delete
        public IActionResult Delete(int id)
        {
            var person = _context.Persons.Find(id);
            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            _context.SaveChanges();
            TempData["Success"] = "Person deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}