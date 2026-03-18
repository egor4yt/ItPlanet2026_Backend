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
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.Organization)
            .HasColumnType("varchar(64)");

        builder
            .HasOne(x => x.Employee)
            .WithMany(x => x.EmployeeEducations)
            .HasForeignKey(x => x.EmployeeId)
            .HasConstraintName("FK_Employee_EmployeeEducations");
    }
}

public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder
            .Property(x => x.Email)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.CompanyName)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.Description)
            .HasColumnType("text");
    }
}