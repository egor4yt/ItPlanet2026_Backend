using Launchpad.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Candidates.Infrastructure.Persistence.Configuration.Entities;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.Title)
            .HasMaxLength(64);

        builder
            .HasMany(x => x.Candidates)
            .WithMany(x => x.Skills)
            .UsingEntity(x => x.ToTable("Candidates_Skills_Map"));
    }
}