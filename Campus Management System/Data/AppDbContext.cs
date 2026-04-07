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
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentModel>()
                .HasOne(s => s.Person)
                .WithOne(p => p.Student)
                .HasForeignKey<StudentModel>(s => s.PersonId);

            modelBuilder.Entity<TeacherModel>()
                .HasOne(t => t.Person)
                .WithOne(p => p.Teacher)
                .HasForeignKey<TeacherModel>(t => t.PersonId);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourse)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourse)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<TeacherCourse>()
                .HasKey(tc => new { tc.TeacherId, tc.CourseId });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourse)
                .HasForeignKey(tc => tc.TeacherId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourse)
                .HasForeignKey(tc => tc.CourseId);
        }
    }
}