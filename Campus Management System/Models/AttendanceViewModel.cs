namespace Campus_Management_System.Models
{
    public class AttendanceVM
    {
        public int CourseId { get; set; }
        public DateTime Date { get; set; }

        public List<int> PresentStudentIds { get; set; } = new();

        public List<StudentAttendanceRow> Students { get; set; } = new();
    }

    public class StudentAttendanceRow
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}