using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campus_Management_System.Models
{
    public class StudentCourseEditVM
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string StudentName { get; set; }

        public List<SelectListItem>? Courses { get; set; }
    }
}