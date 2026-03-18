using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

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

        builder
            .Property(x => x.RegisteredOn)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.PasswordHash)
            .HasColumnType("varchar(64)");
    }
}