using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedAdminCuratorEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Curators",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Email",
                value: "admin@launchpad.ru");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Curators",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Email",
                value: "admin@lauchpad.ru");
        }
    }
}
