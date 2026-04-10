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
            var vm = new PersonEntryViewModel
            {
                Person = new Person(), // important for empty form
                PersonsList = _context.Persons.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePerson(PersonEntryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PersonsList = _context.Persons.ToList();
                TempData["Error"] = "Invalid data!";
                return View("Index", vm);
            }

            if (vm.Person.PersonId == 0)
            {
                _context.Persons.Add(vm.Person);
                TempData["Success"] = "Person Added Successfully!";
            }
            else
            {
                var personInDb = _context.Persons.Find(vm.Person.PersonId);

                if (personInDb == null)
                {
                    TempData["Error"] = "Person not found!";
                    return RedirectToAction("Index");
                }

                personInDb.Name = vm.Person.Name;
                personInDb.Email = vm.Person.Email;
                personInDb.Phone = vm.Person.Phone;
                personInDb.City = vm.Person.City;
                personInDb.Age = vm.Person.Age;
                personInDb.DOB = vm.Person.DOB;
                personInDb.Gender = vm.Person.Gender;
                personInDb.Role = vm.Person.Role;

                TempData["Success"] = "Record Updated Successfully!";
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT (Load data into form)
        public IActionResult Edit(int id)
        {
            var person = _context.Persons.FirstOrDefault(x => x.PersonId == id);

            if (person == null)
                return NotFound();

            var vm = new PersonEntryViewModel
            {
                Person = person,
                PersonsList = _context.Persons.ToList()
            };

            return View("Index", vm);
        }

        // DELETE
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