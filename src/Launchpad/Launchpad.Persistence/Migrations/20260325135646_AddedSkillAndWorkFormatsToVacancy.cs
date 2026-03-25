using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSkillAndWorkFormatsToVacancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Vacancies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Vacancies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VacancySkillMap",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "integer", nullable: false),
                    VacanciesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancySkillMap", x => new { x.SkillsId, x.VacanciesId });
                    table.ForeignKey(
                        name: "FK_VacancySkillMap_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancySkillMap_Vacancies_VacanciesId",
                        column: x => x.VacanciesId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacancyWorkFormatMap",
                columns: table => new
                {
                    VacanciesId = table.Column<long>(type: "bigint", nullable: false),
                    WorkFormatsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyWorkFormatMap", x => new { x.VacanciesId, x.WorkFormatsId });
                    table.ForeignKey(
                        name: "FK_VacancyWorkFormatMap_Vacancies_VacanciesId",
                        column: x => x.VacanciesId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacancyWorkFormatMap_WorkFormat_WorkFormatsId",
                        column: x => x.WorkFormatsId,
                        principalTable: "WorkFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkFormat",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Удаленно" },
                    { 2, "Офис" },
                    { 3, "Гибрид" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacancySkillMap_VacanciesId",
                table: "VacancySkillMap",
                column: "VacanciesId");

            migrationBuilder.CreateIndex(
                name: "IX_VacancyWorkFormatMap_WorkFormatsId",
                table: "VacancyWorkFormatMap",
                column: "WorkFormatsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancySkillMap");

            migrationBuilder.DropTable(
                name: "VacancyWorkFormatMap");

            migrationBuilder.DropTable(
                name: "WorkFormat");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Vacancies");
        }
    }
}
