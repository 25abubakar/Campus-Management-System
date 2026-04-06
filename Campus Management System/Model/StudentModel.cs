namespace Campus_Management_System.Model
{
    public class StudentModel
    {
        public class Student
        {
            public int Id { get; set; }
            public required string Name { get; set; }
            public required string Email { get; set; }
            public required string Phone { get; set; }
            public DateTime DateOfBirth { get; set; }
            public required String Gender { get; set; }

        }
    }
}
