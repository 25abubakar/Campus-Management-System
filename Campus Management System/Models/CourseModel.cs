using System.ComponentModel.DataAnnotations;

namespace Campus_Management_System.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public required string CourseName { get; set; }

        public int CreditHours { get; set; }

        public decimal Fee { get; set; }

        public required List<StudentCourse> StudentCourse { get; set; }
        public required List<TeacherCourse> TeacherCourse { get; set; }
    }
}