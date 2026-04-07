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
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<TeacherCourse> TeacherCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.PersonId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Person)
                .WithMany(p => p.StudentCourse)
                .HasForeignKey(sc => sc.PersonId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourse)
                .HasForeignKey(sc => sc.CourseId);


            modelBuilder.Entity<TeacherCourse>()
                .HasKey(tc => new { tc.PersonId, tc.CourseId });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Person)
                .WithMany(p => p.TeacherCourse)
                .HasForeignKey(tc => tc.PersonId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourse)
                .HasForeignKey(tc => tc.CourseId);
        }
    }
}