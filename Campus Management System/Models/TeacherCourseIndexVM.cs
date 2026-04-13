namespace Campus_Management_System.Models
{
    public class TeacherCourseIndexVM
    {
        public List<TeacherCourse> Enrollments { get; set; }
        public TeacherCourseAssignVM? AssignVM { get; set; }
    }
}