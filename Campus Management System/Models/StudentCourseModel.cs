namespace Campus_Management_System.Models
{
    public class StudentCourse
    {
        public int PersonId { get; set; }
        public required PersonModel Person { get; set; }

        public required int CourseId { get; set; }
        public required  Course Course { get; set; }
    }
}