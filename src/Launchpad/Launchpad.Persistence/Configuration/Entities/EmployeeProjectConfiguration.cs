using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
{
    public void Configure(EntityTypeBuilder<EmployeeProject> builder)
    {
        builder
            .Property(x => x.Description)
            .HasColumnType("text");

        builder
            .Property(x => x.Specialization)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.Link)
            .HasColumnType("varchar(256)")
            .IsRequired(false);

        builder
            .HasOne(x => x.Employee)
            .WithMany(x => x.EmployeeProjects)
            .HasForeignKey(x => x.EmployeeId)
            .HasConstraintName("FK_Employee_EmployeeProjects");

        builder.HasData(
            new EmployeeProject
            {
                Id = -1,
                Title = "Трамплин",
                Description = "Карьерный аггрегатор для студентов",
                Specialization = "React Frontend Developer",
                Link = null,
                DateFrom = new DateOnly(2026, 3, 15),
                DateTo = new DateOnly(2026, 3, 30),
                EmployeeId = -1
            },
            new EmployeeProject
            {
                Id = -2,
                Title = "Finance Helper",
                Description = "Инструмент учета финансов",
                Specialization = "React Frontend Developer",
                Link = null,
                DateFrom = new DateOnly(2024, 6, 1),
                DateTo = new DateOnly(2024, 9, 1),
                EmployeeId = -1
            }
        );
    }
}