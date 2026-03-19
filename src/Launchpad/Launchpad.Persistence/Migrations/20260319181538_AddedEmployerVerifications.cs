using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployerVerifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployerVerificationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerVerificationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerVerifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestMessage = table.Column<string>(type: "text", nullable: false),
                    ResponseMessage = table.Column<string>(type: "text", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerVerificationStatus_EmployerVerifications",
                        column: x => x.StatusId,
                        principalTable: "EmployerVerificationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employer_Verification",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployerVerificationStatuses",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "На рассмотрении" },
                    { 2, "Отказано" },
                    { 3, "Требуются уточнения" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerVerifications_EmployerId",
                table: "EmployerVerifications",
                column: "EmployerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerVerifications_StatusId",
                table: "EmployerVerifications",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployerVerifications");

            migrationBuilder.DropTable(
                name: "EmployerVerificationStatuses");
        }
    }
}
