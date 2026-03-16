using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.FirstName)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.LastName)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.MiddleName)
            .HasColumnType("varchar(64)")
            .IsRequired(false);

        builder
            .Property(x => x.Email)
            .HasColumnType("varchar(64)");

        builder
            .HasIndex(x => x.Email, "UX_Users_Email")
            .IsUnique();

        builder
            .Property(x => x.PasswordHash)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.RegisteredOn)
            .HasColumnType("timestamp without time zone");
    }
}