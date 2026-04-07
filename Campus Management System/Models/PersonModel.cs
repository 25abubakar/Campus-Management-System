using Campus_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Campus_Management_System.Models
{
    public class PersonModel
    {
        public class Person
        {
            public int PersonId { get; set; }

            [Required]
            public string Name { get; set; } = "";
            [Required]
            public string Email { get; set; } = "";
            [Required]
            public string Phone { get; set; } = "";
            [Required]
            public string City { get; set; } = "";

            public int Age { get; set; }
            public string Gender { get; set; } = "";
            public string Role { get; set; } = "";

            public List<StudentCourse> StudentCourses { get; set; } = new();
            public List<TeacherCourse> TeacherCourse { get; set; } = new();

        }
    }
}
   