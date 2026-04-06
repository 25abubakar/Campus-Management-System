using Microsoft.EntityFrameworkCore;
using Campus_Management_System.Model;
namespace Campus_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSets for your tables
        DbSet<Campus_Management_System.Model.StudentModel<> Students { get; set; }
    }
}