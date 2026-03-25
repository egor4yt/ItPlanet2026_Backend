using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class WorkFormatConfiguration : IEntityTypeConfiguration<WorkFormat>
{
    public void Configure(EntityTypeBuilder<WorkFormat> builder)
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
            .WithMany(x => x.WorkFormats)
            .UsingEntity(x => x.ToTable("VacancyWorkFormatMap"));

        builder.HasData(new EducationLevel
        {
            Id = 1,
            Title = "Удаленно"
        }, new EducationLevel
        {
            Id = 2,
            Title = "Офис"
        }, new EducationLevel
        {
            Id = 3,
            Title = "Гибрид"
        });
    }
}