using System.ComponentModel.DataAnnotations;
using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}