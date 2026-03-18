using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(x => x.FirstName)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.LastName)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.MiddleName)
            .HasColumnType("varchar(64)")
            .IsRequired(false);

        builder
            .Property(x => x.Biography)
            .HasColumnType("text");

        builder
            .Property(x => x.Email)
            .HasColumnType("varchar(64)");

        builder
            .HasIndex(x => x.Email, "UX_Users_Email")
            .IsUnique();

        builder
            .Property(x => x.PasswordHash)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.RegisteredOn)
            .HasColumnType("timestamp with time zone");

        builder.HasData(
            new Employee
            {
                Id = -1,
                Email = "kadet_2003@list.ru",
                FirstName = "Артём",
                LastName = "Терешков",
                Biography = "Специализируюсь на экосистеме React: TypeScript, Redux Toolkit, Next.js. За это время успел поработать как над крупными корпоративными проектами (CRM, панели администратора), так и над высоконагруженными публичными сервисами.\n\nОсновной фокус: построение компонентной архитектуры, рефакторинг легаси, внедрение SSR (Next.js) и настройка сборки (Webpack/Vite). Имею опыт наставничества джуниоров и проведения технических интервью. Ищу компанию с прозрачными процессами и современным технологическим стеком, где могу приносить пользу и развиваться дальше.",
                MiddleName = "Вячеславович",
                PasswordHash = "0D73C0A5D54B086B544B1A76A121CAE545B6A204F6D85E4CB68A0786991FEC67",
                RegisteredOn = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}