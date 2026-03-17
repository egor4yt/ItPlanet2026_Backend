using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevel>
{
    public void Configure(EntityTypeBuilder<EducationLevel> builder)
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
            .HasMany(x => x.EmployeeEducations)
            .WithOne(x => x.EducationLevel)
            .HasForeignKey(x => x.EducationLevelId)
            .HasConstraintName("FK_EducationLevel_EmployeeEducations");

        builder.HasData(new EducationLevel
        {
            Id = 1,
            Title = "Среднее"
        }, new EducationLevel
        {
            Id = 2,
            Title = "Среднее специальное"
        }, new EducationLevel
        {
            Id = 3,
            Title = "Неоконченное высшее"
        }, new EducationLevel
        {
            Id = 4,
            Title = "Высшее"
        }, new EducationLevel
        {
            Id = 5,
            Title = "Бакалавр"
        }, new EducationLevel
        {
            Id = 6,
            Title = "Магистр"
        }, new EducationLevel
        {
            Id = 7,
            Title = "Кандидат наук"
        }, new EducationLevel
        {
            Id = 8,
            Title = "Доктор наук"
        });
    }
}