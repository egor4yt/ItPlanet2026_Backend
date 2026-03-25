using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkFormatsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyWorkFormatMap_WorkFormat_WorkFormatsId",
                table: "VacancyWorkFormatMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFormat",
                table: "WorkFormat");

            migrationBuilder.RenameTable(
                name: "WorkFormat",
                newName: "WorkFormats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFormats",
                table: "WorkFormats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyWorkFormatMap_WorkFormats_WorkFormatsId",
                table: "VacancyWorkFormatMap",
                column: "WorkFormatsId",
                principalTable: "WorkFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacancyWorkFormatMap_WorkFormats_WorkFormatsId",
                table: "VacancyWorkFormatMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkFormats",
                table: "WorkFormats");

            migrationBuilder.RenameTable(
                name: "WorkFormats",
                newName: "WorkFormat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkFormat",
                table: "WorkFormat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VacancyWorkFormatMap_WorkFormat_WorkFormatsId",
                table: "VacancyWorkFormatMap",
                column: "WorkFormatsId",
                principalTable: "WorkFormat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
