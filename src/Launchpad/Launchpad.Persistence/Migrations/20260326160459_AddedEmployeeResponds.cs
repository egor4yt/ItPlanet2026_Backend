using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeResponds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false),
                    Code = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRespondStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false),
                    Description = table.Column<string>(type: "varchar(64)", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRespondStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_EmployeeRespondStatuses",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeResponds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CoverMessage = table.Column<string>(type: "text", nullable: true),
                    CompanyAnswer = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    VacancyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeResponds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeResponds",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Status_EmployeeResponds",
                        column: x => x.StatusId,
                        principalTable: "EmployeeRespondStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancy_EmployeeResponds",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Code", "Title" },
                values: new object[,]
                {
                    { 1, "#DC2626", "Ошибка" },
                    { 2, "#D97706", "Предупреждение" },
                    { 3, "#16A34A", "Успех" },
                    { 4, "#6B7280", "Нейтральный" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRespondStatuses",
                columns: new[] { "Id", "ColorId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Вам отказали", "Отказ" },
                    { 2, 4, "Отклик", "Не просмотрено" },
                    { 3, 3, "Вас пригласили на собеседование", "Приглашение на собеседование" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResponds_EmployeeId",
                table: "EmployeeResponds",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResponds_StatusId",
                table: "EmployeeResponds",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResponds_VacancyId",
                table: "EmployeeResponds",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRespondStatuses_ColorId",
                table: "EmployeeRespondStatuses",
                column: "ColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeResponds");

            migrationBuilder.DropTable(
                name: "EmployeeRespondStatuses");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
