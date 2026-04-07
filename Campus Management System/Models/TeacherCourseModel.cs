using Campus_Management_System.Models;

public class TeacherCourse
{
    public int TeacherId { get; set; }
    public required TeacherModel Teacher { get; set; }

    public int CourseId { get; set; }
    public required Course Course { get; set; }
}