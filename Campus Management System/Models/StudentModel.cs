using Campus_Management_System.Models;

public class StudentModel
{
    public int StudentId { get; set; }

    public int PersonId { get; set; }
    public required PersonModel Person { get; set; }

    public required string RollNumber { get; set; }
    public required string Class { get; set; }
    public required string Grade { get; set; }

    public List<StudentCourse> StudentCourse { get; set; } = new();
}