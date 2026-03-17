using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEducations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Organization = table.Column<string>(type: "varchar(64)", nullable: false),
                    Faculty = table.Column<string>(type: "varchar(64)", nullable: false),
                    Specialization = table.Column<string>(type: "varchar(64)", nullable: false),
                    CompletionYear = table.Column<int>(type: "integer", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationLevel_EmployeeEducations",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeEducations",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EducationLevels",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Среднее" },
                    { 2, "Среднее специальное" },
                    { 3, "Неоконченное высшее" },
                    { 4, "Высшее" },
                    { 5, "Бакалавр" },
                    { 6, "Магистр" },
                    { 7, "Кандидат наук" },
                    { 8, "Доктор наук" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EducationLevelId",
                table: "EmployeeEducations",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeId",
                table: "EmployeeEducations",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEducations");

            migrationBuilder.DropTable(
                name: "EducationLevels");
        }
    }
}
