namespace Campus_Management_System.Models
{
    public class TeacherCreateViewModel
    {
        public Teacher Teacher { get; set; } = new Teacher();
        public List<Person>? TeacherPersons { get; set; } = new List<Person>();
        public List<Course>? Courses { get; set; } = new List<Course>();
        public List<int>? SelectedCourseIds { get; set; } = new List<int>();
    }
}