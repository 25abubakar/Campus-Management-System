using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Campus_Management_System.Models
{
    [Index(nameof(CourseName), IsUnique = true)]
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        public int CreditHours { get; set; }

        public decimal Fee { get; set; }

        public List<StudentCourse>? StudentCourse { get; set; }
        public List<TeacherCourse>? TeacherCourse { get; set; }
    }
}