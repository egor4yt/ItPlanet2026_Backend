using Launchpad.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Candidates.Infrastructure.Persistence.Configuration.Entities;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Navigation(x => x.Skills)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Property(x => x.Biography)
            .HasMaxLength(2000);

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(128);

        builder
            .Property(x => x.LastName)
            .HasMaxLength(128);

        builder
            .Property(x => x.MiddleName)
            .HasMaxLength(128);
    }
}