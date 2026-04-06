using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Campus_Management_System.Model
{
    public class CourseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Credit Hours are required")]
        [Range(1, 10, ErrorMessage = "Credit Hours must be between 1 and 10")]
        public int CreditHours { get; set; }

        [Required(ErrorMessage = "Fee is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Fee must be positive")]
        public decimal Fee { get; set; }
    }
}



// Pages/View/Students.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Campus_Management_System.Models; // adjust if your Student model lives elsewhere

namespace Campus_Management_System.Pages.View
{
    public class StudentsModel : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }

        public void OnGet()
        {
            Student = new Student();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            // persist Student...
            return RedirectToPage("/Index");
        }
    }
}
// Models/Student.cs
namespace Campus_Management_System.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}