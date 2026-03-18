using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployerPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Employers",
                type: "varchar(64)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "Employers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "Employers");
        }
    }
}
