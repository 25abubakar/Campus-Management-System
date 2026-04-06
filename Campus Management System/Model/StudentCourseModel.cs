using static Campus_Management_System.Models.CourseModel;

namespace Campus_Management_System.Models
{
    public class StudentCourseModel
    {
        public int StudentId { get; set; }
        public required StudentModel.Student Student { get; set; }

        public required int CourseId { get; set; }
        public required  Course Course { get; set; }
    }
}