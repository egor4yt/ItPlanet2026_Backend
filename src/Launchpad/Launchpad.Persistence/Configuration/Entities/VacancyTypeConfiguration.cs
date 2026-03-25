using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class VacancyTypeConfiguration : IEntityTypeConfiguration<VacancyType>
{
    public void Configure(EntityTypeBuilder<VacancyType> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder
            .HasMany(x => x.Vacancies)
            .WithOne(x => x.Type)
            .HasForeignKey(x => x.TypeId)
            .HasConstraintName("FK_Vacancies_VacancyType");

        builder.HasData(new EducationLevel
        {
            Id = 1,
            Title = "Стажировка"
        }, new EducationLevel
        {
            Id = 2,
            Title = "Вакансия"
        }, new EducationLevel
        {
            Id = 3,
            Title = "Менторинг"
        }, new EducationLevel
        {
            Id = 4,
            Title = "Мероприятие"
        });
    }
}