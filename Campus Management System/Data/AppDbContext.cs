using Microsoft.EntityFrameworkCore;
using Campus_Management_System.Models;

namespace Campus_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PersonCourse> PersonCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonCourse>()
                .HasKey(pc => new { pc.PersonId, pc.CourseId });

            modelBuilder.Entity<PersonCourse>()
                .HasOne(pc => pc.Person)
                .WithMany(p => p.PersonCourses)
                .HasForeignKey(pc => pc.PersonId);

            modelBuilder.Entity<PersonCourse>()
                .HasOne(pc => pc.Course)
                .WithMany(c => c.PersonCourses)
                .HasForeignKey(pc => pc.CourseId);
        }
    }
}