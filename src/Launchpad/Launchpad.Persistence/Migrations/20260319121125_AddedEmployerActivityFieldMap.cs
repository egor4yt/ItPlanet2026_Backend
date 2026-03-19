using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployerActivityFieldMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Employers",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityField_Employer_Map",
                columns: table => new
                {
                    ActivityFieldsId = table.Column<int>(type: "integer", nullable: false),
                    EmployersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityField_Employer_Map", x => new { x.ActivityFieldsId, x.EmployersId });
                    table.ForeignKey(
                        name: "FK_ActivityField_Employer_Map_ActivityFields_ActivityFieldsId",
                        column: x => x.ActivityFieldsId,
                        principalTable: "ActivityFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityField_Employer_Map_Employers_EmployersId",
                        column: x => x.EmployersId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Employers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Website",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityField_Employer_Map_EmployersId",
                table: "ActivityField_Employer_Map",
                column: "EmployersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityField_Employer_Map");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Employers");
        }
    }
}
