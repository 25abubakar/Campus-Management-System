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
        public string RollNumber { get; set; } = string.Empty;

        public Person? Person { get; set; }
        public ICollection<StudentCourse>? StudentCourse { get; set; }
}

}
