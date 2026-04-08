using System.ComponentModel.DataAnnotations;

namespace CampusManagementSystem.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string City { get; set; }

        public int Age { get; set; }
        public DateTime? DOB { get; set; }
        public required string Gender { get; set; }
        public required string Role { get; set; }


        public Student? Students { get; set; }
        public Teacher? Teachers { get; set; }

    }
}