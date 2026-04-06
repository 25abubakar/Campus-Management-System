namespace Campus_Management_System.Models
{
    public class Student
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }

        // Navigation Property
        public List<StudentCourseModel> StudentCourses { get; set; } = new();
    }
}