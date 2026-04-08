using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class CourseEntryViewModel
    {
        public Course Course { get; set; } = new Course();
        public List<Course>? CoursesList { get; set; } = new List<Course>();
    }
}