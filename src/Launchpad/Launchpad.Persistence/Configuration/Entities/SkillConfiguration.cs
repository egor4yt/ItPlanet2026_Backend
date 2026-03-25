using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)");

        builder
            .HasMany(x => x.Employees)
            .WithMany(x => x.Skills)
            .UsingEntity(x => x.ToTable("EmployeeSkillMap"));

        builder
            .HasMany(x => x.Vacancies)
            .WithMany(x => x.Skills)
            .UsingEntity(x => x.ToTable("VacancySkillMap"));

        builder.HasData(new Skill
        {
            Id = -1,
            Title = "React",
            IsSystemTag = true
        }, new Skill
        {
            Id = -2,
            Title = ".NET",
            IsSystemTag = true
        }, new Skill
        {
            Id = -3,
            Title = "Git",
            IsSystemTag = true
        }, new Skill
        {
            Id = -4,
            Title = "HTML",
            IsSystemTag = true
        }, new Skill
        {
            Id = -5,
            Title = "CSS",
            IsSystemTag = true
        }, new Skill
        {
            Id = -6,
            Title = "SOLID",
            IsSystemTag = true
        }, new Skill
        {
            Id = -7,
            Title = "Entity framework core",
            IsSystemTag = true
        });
    }
}