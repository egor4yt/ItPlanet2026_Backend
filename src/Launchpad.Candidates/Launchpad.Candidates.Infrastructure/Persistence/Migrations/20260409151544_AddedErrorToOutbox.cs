using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Candidates.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedErrorToOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "OutboxMessages",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                table: "OutboxMessages");
        }
    }
}
