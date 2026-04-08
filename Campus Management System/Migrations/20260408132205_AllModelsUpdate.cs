using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campus_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AllModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeNumber",
                table: "Teacher",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "Depatment",
                table: "Teacher",
                newName: "Qualification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Teacher",
                newName: "EmployeeNumber");

            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "Teacher",
                newName: "Depatment");
        }
    }
}
