using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campus_Management_System.Models
{
    public class StudentCourseAssignVM
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }
        public int? RollNumber { get; set; }
        public string Class { get; set; }
        public string Grade { get; set; }

        public List<int> SelectedCourses { get; set; } = new List<int>();

        public List<SelectListItem>? Courses { get; set; }
    }
}