using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Candidates.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTypeToOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "OutboxMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "OutboxMessages");
        }
    }
}
