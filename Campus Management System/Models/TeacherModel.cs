using System.ComponentModel.DataAnnotations;
using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public int PersonId { get; set; }

        public string Depatment { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public Person? Person { get; set; }
        public ICollection<TeacherCourse>? TeacherCourse { get; set; }
    }
}

