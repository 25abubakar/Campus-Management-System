using Campus_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System;


namespace Campus_Management_System.Pages.View
{
    public class StudentModel : PageModel
    {
        [BindProperty]
        public StudentModel Student { get; set; } = new StudentModel(); // ✅ initialize to avoid non-nullable error

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/View/Students");
        }
    }
}


namespace Campus_Management_System.Model
{
    public class StudentModel
    {

        [Required]
        public string Name { get; set; } = string.Empty; // ✅ initialized

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("Male|Female|Other")]
        public string Gender { get; set; } = string.Empty;

        public int Age => DateTime.Today.Year - DateOfBirth.Year;
    }
}