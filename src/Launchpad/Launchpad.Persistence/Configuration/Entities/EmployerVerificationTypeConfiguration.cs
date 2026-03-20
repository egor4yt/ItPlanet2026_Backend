using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployerVerificationTypeConfiguration : IEntityTypeConfiguration<EmployerVerificationType>
{
    public void Configure(EntityTypeBuilder<EmployerVerificationType> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.HtmlDescription)
            .HasColumnType("text");
        
        builder
            .HasMany(x => x.EmployerVerifications)
            .WithOne(x => x.Type)
            .HasForeignKey(x => x.EmployerVerificationTypeId)
            .HasConstraintName("FK_EmployerVerifications_EmployerVerificationType");

        builder.HasData(
            new EmployerVerificationType
            {
                Id = 1,
                Title = "Организация",
                HtmlDescription = """
                                  <p>Отправьте один из документов:</p>
                                  <ul>
                                    <li>Свидетельство о государственной регистрации ЮЛ/ИП</li>
                                    <li>Свидетельство о постановке на налоговый учёт ЮЛ/ИП</li>
                                    <li>Лист записи ЕГРЮЛ/ЕГРИП, его можно получить в личном кабинете на <a href="https://nalog.gov.ru">nalog.gov.ru</a></li>
                                  </ul>
                                  """
            },
            new EmployerVerificationType
            {
                Id = 2,
                Title = "Кадровое агентство",
                HtmlDescription = """
                                  <p>Отправьте один из документов:</p>
                                  <ul>
                                    <li>Свидетельство о государственной регистрации ЮЛ/ИП</li>
                                    <li>Свидетельство о постановке на налоговый учёт ЮЛ/ИП</li>
                                    <li>Лист записи ЕГРЮЛ/ЕГРИП, его можно получить в личном кабинете на <a href="https://nalog.gov.ru">nalog.gov.ru</a></li>
                                  </ul>
                                  """
            },
            new EmployerVerificationType
            {
                Id = 3,
                Title = "Проект",
                HtmlDescription = "<p>Загрузите фото или скан листа записи ЕГРЮЛ или ЕГРИП о создании юрлица или регистрации ИП</p>"
            },
            new EmployerVerificationType
            {
                Id = 4,
                Title = "Частное лицо",
                HtmlDescription = "Загрузите скан или фото первого разворота паспорта"
            },
            new EmployerVerificationType
            {
                Id = 5,
                Title = "Самозанятый",
                HtmlDescription = """
                                  <p>Отправьте один из документов:</p>
                                  <ul>
                                    <li>Разворот паспорта с фотографией</li>
                                    <li>Фото или скан справки о регистрации плательщика НПД</li>
                                  </ul>
                                  """
            },
            new EmployerVerificationType
            {
                Id = 6,
                Title = "Кадровое агентство",
                HtmlDescription = """
                                  <p>Отправьте один из документов:</p>
                                  <ul>
                                    <li>Разворот паспорта с фотографией</li>
                                    <li>Фото или скан справки о регистрации плательщика НПД</li>
                                  </ul>
                                  """
            }
        );
    }
}