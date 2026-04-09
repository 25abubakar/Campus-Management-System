using Campus_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<StudentCourse>().ToTable("StudentCourse");
            modelBuilder.Entity<TeacherCourse>().ToTable("TeacherCourse");

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Person)
                .WithOne(p => p.Students)
                .HasForeignKey<Student>(s => s.PersonId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Person)
                .WithOne(p => p.Teachers)
                .HasForeignKey<Teacher>(t => t.PersonId);

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

            modelBuilder.Entity<Course>()
                .Property(c => c.Fee)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasColumnType("decimal(18,2)");
        }
    }
}