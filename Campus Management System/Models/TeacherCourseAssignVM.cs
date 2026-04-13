using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campus_Management_System.Models
{
    public class TeacherCourseAssignVM
    {
        public int TeacherId { get; set; }

        public string TeacherName { get; set; }
        public string Qualification { get; set; }
        public decimal Salary { get; set; }

        public List<int> SelectedCourses { get; set; } = new();

        public List<SelectListItem>? Courses { get; set; }
    }
}