using System.ComponentModel.DataAnnotations;
using Campus_Management_System.Models;

namespace Campus_Management_System.Models
{
public class Student
{
    public int StudentId { get; set; }

    public int PersonId { get; set; }
    public Person? Person { get; set; }

    public required string RollNumber { get; set; }
    public required string Class { get; set; }
    public required string Grade { get; set; }

    public List<StudentCourse> StudentCourse { get; set; } = new();
}
}
