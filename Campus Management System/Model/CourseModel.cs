using System.ComponentModel.DataAnnotations;

namespace Campus_Management_System.Model
{
    public class CourseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Credit Hours are required")]
        [Range(1, 10, ErrorMessage = "Credit Hours must be between 1 and 10")]
        public int CreditHours { get; set; }

        [Required(ErrorMessage = "Fee is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Fee must be positive")]
        public decimal Fee { get; set; }
    }
}