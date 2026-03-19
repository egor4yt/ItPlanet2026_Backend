using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class CuratorConfiguration : IEntityTypeConfiguration<Curator>
{
    public void Configure(EntityTypeBuilder<Curator> builder)
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
            .HasIndex(x => x.Email, "UX_Curator_Email")
            .IsUnique();

        builder
            .Property(x => x.PasswordHash)
            .HasColumnType("varchar(64)");

        builder.HasData(new Curator
        {
            Id = -1,
            Email = "admin@launchpad.ru",
            FirstName = "Администратор",
            LastName = "",
            MiddleName = "",
            PasswordHash = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918",
            IsAdmin = true
        });
    }
}