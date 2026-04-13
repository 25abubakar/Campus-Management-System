
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class FixTeacherCourseRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherCourseCourseId",
                table: "TeacherCourse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherCourseTeacherId",
                table: "TeacherCourse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourse_TeacherCourseTeacherId_TeacherCourseCourseId",
                table: "TeacherCourse",
                columns: new[] { "TeacherCourseTeacherId", "TeacherCourseCourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherCourse_TeacherCourse_TeacherCourseTeacherId_TeacherCourseCourseId",
                table: "TeacherCourse",
                columns: new[] { "TeacherCourseTeacherId", "TeacherCourseCourseId" },
                principalTable: "TeacherCourse",
                principalColumns: new[] { "TeacherId", "CourseId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherCourse_TeacherCourse_TeacherCourseTeacherId_TeacherCourseCourseId",
                table: "TeacherCourse");

            migrationBuilder.DropIndex(
                name: "IX_TeacherCourse_TeacherCourseTeacherId_TeacherCourseCourseId",
                table: "TeacherCourse");

            migrationBuilder.DropColumn(
                name: "TeacherCourseCourseId",
                table: "TeacherCourse");

            migrationBuilder.DropColumn(
                name: "TeacherCourseTeacherId",
                table: "TeacherCourse");
        }
    }
}
