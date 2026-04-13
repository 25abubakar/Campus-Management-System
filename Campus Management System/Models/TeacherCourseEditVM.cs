using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campus_Management_System.Models
{
    public class TeacherCourseEditVM
    {
        public int TeacherId { get; set; }

        public int OldCourseId { get; set; }

        public int CourseId { get; set; }

        public string TeacherName { get; set; }

        public List<SelectListItem>? Courses { get; set; }
    }
}