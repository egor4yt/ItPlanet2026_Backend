using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(128)");

        builder
            .Property(x => x.City)
            .HasColumnType("varchar(128)");

        builder
            .Property(x => x.FullAddress)
            .HasColumnType("varchar(1024)");

        builder
            .Property(x => x.Description)
            .HasColumnType("text");

        builder
            .Property(x => x.Location)
            .HasColumnType("geography (point, 4326)");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.StartDate)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.EndDate)
            .HasColumnType("timestamp with time zone");
        
        builder
            .HasIndex(x => x.CreatedAt)
            .HasDatabaseName("IX_Vacancy_createdAt");

        builder
            .HasOne(x => x.Employer)
            .WithMany(x => x.Vacancies)
            .HasForeignKey(y => y.EmployerId)
            .HasConstraintName("FK_Employer_Vacancies")
            .OnDelete(DeleteBehavior.Cascade);
    }
}