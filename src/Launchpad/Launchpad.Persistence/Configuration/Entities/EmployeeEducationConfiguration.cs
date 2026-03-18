using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployeeEducationConfiguration : IEntityTypeConfiguration<EmployeeEducation>
{
    public void Configure(EntityTypeBuilder<EmployeeEducation> builder)
    {
        builder
            .Property(x => x.Faculty)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.Specialization)
            .HasColumnType("varchar(256)");

        builder
            .Property(x => x.Organization)
            .HasColumnType("varchar(64)");

        builder
            .HasOne(x => x.Employee)
            .WithMany(x => x.EmployeeEducations)
            .HasForeignKey(x => x.EmployeeId)
            .HasConstraintName("FK_Employee_EmployeeEducations");


        builder.HasData(
            new EmployeeEducation
            {
                Id = -1,
                Organization = "МФТИ",
                Faculty = "Факультет информационных технологий",
                Specialization = "Веб-разработка",
                CompletionYear = 2024,
                EducationLevelId = 6,
                EmployeeId = -1
            }
        );
    }
}