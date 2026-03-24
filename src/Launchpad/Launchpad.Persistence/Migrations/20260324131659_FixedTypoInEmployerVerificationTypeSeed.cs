using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypoInEmployerVerificationTypeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployerVerificationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Частный рекрутер");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployerVerificationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Кадровое агентство");
        }
    }
}
