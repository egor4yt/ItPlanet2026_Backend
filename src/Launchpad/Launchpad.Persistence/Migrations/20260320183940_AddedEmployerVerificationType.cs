using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployerVerificationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployerVerificationTypeId",
                table: "EmployerVerifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SocialNetworkLink",
                table: "EmployerVerifications",
                type: "varchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxpayerIndividualNumber",
                table: "EmployerVerifications",
                type: "varchar(12)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployerVerificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(64)", nullable: false),
                    HtmlDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerVerificationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmployerVerificationStatuses",
                columns: new[] { "Id", "Title" },
                values: new object[] { 4, "Подтверждено" });

            migrationBuilder.InsertData(
                table: "EmployerVerificationTypes",
                columns: new[] { "Id", "HtmlDescription", "Title" },
                values: new object[,]
                {
                    { 1, "<p>Отправьте один из документов:</p>\r\n<ul>\r\n  <li>Свидетельство о государственной регистрации ЮЛ/ИП</li>\r\n  <li>Свидетельство о постановке на налоговый учёт ЮЛ/ИП</li>\r\n  <li>Лист записи ЕГРЮЛ/ЕГРИП, его можно получить в личном кабинете на <a href=\"https://nalog.gov.ru\">nalog.gov.ru</a></li>\r\n</ul>", "Организация" },
                    { 2, "<p>Отправьте один из документов:</p>\r\n<ul>\r\n  <li>Свидетельство о государственной регистрации ЮЛ/ИП</li>\r\n  <li>Свидетельство о постановке на налоговый учёт ЮЛ/ИП</li>\r\n  <li>Лист записи ЕГРЮЛ/ЕГРИП, его можно получить в личном кабинете на <a href=\"https://nalog.gov.ru\">nalog.gov.ru</a></li>\r\n</ul>", "Кадровое агентство" },
                    { 3, "<p>Загрузите фото или скан листа записи ЕГРЮЛ или ЕГРИП о создании юрлица или регистрации ИП</p>", "Проект" },
                    { 4, "Загрузите скан или фото первого разворота паспорта", "Частное лицо" },
                    { 5, "<p>Отправьте один из документов:</p>\r\n<ul>\r\n  <li>Разворот паспорта с фотографией</li>\r\n  <li>Фото или скан справки о регистрации плательщика НПД</li>\r\n</ul>", "Самозанятый" },
                    { 6, "<p>Отправьте один из документов:</p>\r\n<ul>\r\n  <li>Разворот паспорта с фотографией</li>\r\n  <li>Фото или скан справки о регистрации плательщика НПД</li>\r\n</ul>", "Кадровое агентство" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerVerifications_EmployerVerificationTypeId",
                table: "EmployerVerifications",
                column: "EmployerVerificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerVerifications_EmployerVerificationType",
                table: "EmployerVerifications",
                column: "EmployerVerificationTypeId",
                principalTable: "EmployerVerificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerVerifications_EmployerVerificationType",
                table: "EmployerVerifications");

            migrationBuilder.DropTable(
                name: "EmployerVerificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployerVerifications_EmployerVerificationTypeId",
                table: "EmployerVerifications");

            migrationBuilder.DeleteData(
                table: "EmployerVerificationStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "EmployerVerificationTypeId",
                table: "EmployerVerifications");

            migrationBuilder.DropColumn(
                name: "SocialNetworkLink",
                table: "EmployerVerifications");

            migrationBuilder.DropColumn(
                name: "TaxpayerIndividualNumber",
                table: "EmployerVerifications");
        }
    }
}
