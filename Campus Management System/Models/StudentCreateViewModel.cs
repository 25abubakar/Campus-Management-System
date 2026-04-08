using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class StudentCreateViewModel
    {
        public Student Student { get; set; } = new Student();
        public List<Person>? StudentPersons { get; set; } = new List<Person>();
        public List<Course>? Courses { get; set; } = new List<Course>();
        public List<int>? SelectedCourseIds { get; set; } = new List<int>();
    }
}