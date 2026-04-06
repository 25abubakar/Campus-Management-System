namespace Campus_Management_System.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public required string CourseName { get; set; }
        public int CreditHours { get; set; }
        public decimal Fee { get; set; }

        // Navigation Property
        public List<StudentCourseModel> StudentCourses { get; set; } = new();
    }
}