using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VacancyIndexOptimization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeResponds_EmployeeId",
                table: "EmployeeResponds");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_createdAt",
                table: "Vacancies",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "UX_EmployeeRespond_EmployeeVacancy",
                table: "EmployeeResponds",
                columns: new[] { "EmployeeId", "VacancyId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vacancy_createdAt",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "UX_EmployeeRespond_EmployeeVacancy",
                table: "EmployeeResponds");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResponds_EmployeeId",
                table: "EmployeeResponds",
                column: "EmployeeId");
        }
    }
}
