namespace Campus_Management_System.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        public int PersonId { get; set; }
        public string Qualification { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public Person? Person { get; set; }
        public ICollection<TeacherCourse>? TeacherCourse { get; set; }
    }
}