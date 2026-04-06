namespace Campus_Management_System.Models
{
    public class StudentCourseModel
    {
        public int StudentId { get; set; }
        public required Student Student { get; set; }

        public required int CourseId { get; set; }
        public required  Course Course { get; set; }
    }
}