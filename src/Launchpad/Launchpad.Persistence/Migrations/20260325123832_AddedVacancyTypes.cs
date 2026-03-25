using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedVacancyTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacancyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VacancyTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Стажировка" },
                    { 2, "Вакансия" },
                    { 3, "Менторинг" },
                    { 4, "Мероприятие" }
                });
            
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Vacancies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_TypeId",
                table: "Vacancies",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_VacancyType",
                table: "Vacancies",
                column: "TypeId",
                principalTable: "VacancyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.Sql("""
                                 UPDATE public."Vacancies" SET "TypeId" = 1 WHERE "TypeId" IS NULL;
                                 """);
            
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Vacancies",
                type: "integer",
                nullable: false,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_VacancyType",
                table: "Vacancies");

            migrationBuilder.DropTable(
                name: "VacancyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_TypeId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Vacancies");
        }
    }
}
