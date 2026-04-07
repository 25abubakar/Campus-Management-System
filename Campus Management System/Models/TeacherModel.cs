using Campus_Management_System.Models;

public class TeacherModel
{
    public int TeacherId { get; set; }

    public int PersonId { get; set; }
    public required PersonModel Person { get; set; }

    public required string EmployeeNumber { get; set; }
    public required string Depatment { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }

    public List<TeacherCourse> TeacherCourse { get; set; } = new();
}