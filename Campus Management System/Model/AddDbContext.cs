internal class AddDbContext
{
public class CourseModel
    {
        public class Course
        {
            public int CourseId { get; set; }
            public required string CourseName { get; set; }
            public int CreditHours { get; set; }
            public Decimal Fee { get; set; }
        }
    }
}

