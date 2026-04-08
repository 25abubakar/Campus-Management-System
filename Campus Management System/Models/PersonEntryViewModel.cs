using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
    public class PersonEntryViewModel
    {
        public Person Person { get; set; } = new Person();
        public Student? Student { get; set; } = new Student();
        public Teacher? Teacher { get; set; } = new Teacher();
        public List<int>? SelectedCourseIds { get; set; } // View mein checkboxes ke liye
    }

}
