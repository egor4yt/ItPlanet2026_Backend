using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Launchpad.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeededAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Biography", "Email", "FirstName", "LastName", "MiddleName", "PasswordHash", "RegisteredOn" },
                values: new object[] { -1L, "Специализируюсь на экосистеме React: TypeScript, Redux Toolkit, Next.js. За это время успел поработать как над крупными корпоративными проектами (CRM, панели администратора), так и над высоконагруженными публичными сервисами.\n\nОсновной фокус: построение компонентной архитектуры, рефакторинг легаси, внедрение SSR (Next.js) и настройка сборки (Webpack/Vite). Имею опыт наставничества джуниоров и проведения технических интервью. Ищу компанию с прозрачными процессами и современным технологическим стеком, где могу приносить пользу и развиваться дальше.", "kadet_2003@list.ru", "Артём", "Терешков", "Вячеславович", "0D73C0A5D54B086B544B1A76A121CAE545B6A204F6D85E4CB68A0786991FEC67", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "CompanyName", "Description", "Email", "PasswordHash", "RegisteredOn" },
                values: new object[] { -1L, "Трамплин", "Платформа «Трамплин» должна стать экосистемой, где студенты не просто ищут работу, а строят карьеру с нуля: находят менторов, участвуют в карьерных мероприятиях компаний и получают предложения о стажировках, основываясь на своих навыках и активности. Некоторые функциональные и нефункциональные требования описаны заказчиком напрямую, по другим же функциональным и нефункциональным требованиям вам необходимо выработать решения самостоятельно и аргументировать их перед заказчиком", "kadet_2003@list.ru", "0D73C0A5D54B086B544B1A76A121CAE545B6A204F6D85E4CB68A0786991FEC67", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "EmployeeEducations",
                columns: new[] { "Id", "CompletionYear", "EducationLevelId", "EmployeeId", "Faculty", "Organization", "Specialization" },
                values: new object[] { -1L, 2024, 6, -1L, "Факультет информационных технологий", "МФТИ", "Веб-разработка" });

            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "Id", "DateFrom", "DateTo", "Description", "EmployeeId", "Link", "Specialization", "Title" },
                values: new object[,]
                {
                    { -2L, new DateOnly(2024, 6, 1), new DateOnly(2024, 9, 1), "Инструмент учета финансов", -1L, null, "React Frontend Developer", "Finance Helper" },
                    { -1L, new DateOnly(2026, 3, 15), new DateOnly(2026, 3, 30), "Карьерный аггрегатор для студентов", -1L, null, "React Frontend Developer", "Трамплин" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeEducations",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "EmployeeProjects",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "EmployeeProjects",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "Employers",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: -1L);
        }
    }
}
