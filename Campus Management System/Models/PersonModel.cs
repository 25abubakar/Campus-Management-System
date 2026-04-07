using Campus_Management_System.Models;

public class PersonModel
{
    public int PersonId { get; set; }

    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string City { get; set; }

    public int Age { get; set; }
    public required string Gender { get; set; }
    public required string Role { get; set; }

    public List<StudentModel> Students { get; set; } = new();
    public List<TeacherModel> Teachers { get; set; } = new();
}