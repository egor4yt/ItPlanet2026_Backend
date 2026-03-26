using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployeeRespondConfiguration : IEntityTypeConfiguration<EmployeeRespond>
{
    public void Configure(EntityTypeBuilder<EmployeeRespond> builder)
    {
        builder
            .Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.CoverMessage)
            .HasColumnType("text");

        builder
            .Property(x => x.CompanyAnswer)
            .HasColumnType("text");

        builder
            .HasOne(x => x.Status)
            .WithMany(x => x.EmployeeResponds)
            .HasForeignKey(x => x.StatusId)
            .HasConstraintName("FK_Status_EmployeeResponds");

        builder
            .HasOne(x => x.Employee)
            .WithMany(x => x.EmployeeResponds)
            .HasForeignKey(x => x.EmployeeId)
            .HasConstraintName("FK_Employee_EmployeeResponds");

        builder
            .HasOne(x => x.Vacancy)
            .WithMany(x => x.EmployeeResponds)
            .HasForeignKey(x => x.VacancyId)
            .HasConstraintName("FK_Vacancy_EmployeeResponds");
    }
}