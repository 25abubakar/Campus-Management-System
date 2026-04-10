using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Campus_Management_System.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public int Age { get; set; }
        public DateTime? DOB { get; set; }

        public Student? Students { get; set; }
        public Teacher? Teachers { get; set; }

    }
}