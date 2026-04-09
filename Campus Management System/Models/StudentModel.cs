using System.ComponentModel.DataAnnotations;
using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int PersonId { get; set; }

        public string Class { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public int RollNumber { get; set; } 

        public Person? Person { get; set; }
        public ICollection<StudentCourse>? StudentCourse { get; set; }
}

}
