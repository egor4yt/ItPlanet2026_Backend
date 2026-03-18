using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTableProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationProjects",
                table: "EducationProjects");

            migrationBuilder.RenameTable(
                name: "EducationProjects",
                newName: "EmployeeProjects");

            migrationBuilder.RenameIndex(
                name: "IX_EducationProjects_EmployeeId",
                table: "EmployeeProjects",
                newName: "IX_EmployeeProjects_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjects",
                table: "EmployeeProjects");

            migrationBuilder.RenameTable(
                name: "EmployeeProjects",
                newName: "EducationProjects");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjects_EmployeeId",
                table: "EducationProjects",
                newName: "IX_EducationProjects_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationProjects",
                table: "EducationProjects",
                column: "Id");
        }
    }
}
