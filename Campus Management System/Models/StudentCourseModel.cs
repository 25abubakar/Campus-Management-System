using Campus_Management_System.Models;

public class StudentCourse
{
    public int StudentId { get; set; }
    public required StudentModel Student { get; set; }

    public int CourseId { get; set; }
    public required Course Course { get; set; }
}