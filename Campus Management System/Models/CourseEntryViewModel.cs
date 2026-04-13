using Campus_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Campus_Management_System.Models
{
    public class CourseEntryViewModel
    {
        public Course Course { get; set; } = new Course();

        public List<Course>? CoursesList { get; set; } = new List<Course>();

    }
}