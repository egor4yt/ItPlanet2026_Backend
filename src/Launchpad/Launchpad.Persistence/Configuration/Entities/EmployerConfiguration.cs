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

        builder
            .Property(x => x.Website)
            .HasColumnType("varchar(256)")
            .IsRequired(false);

        builder
            .HasMany(x => x.ActivityFields)
            .WithMany(x => x.Employers)
            .UsingEntity(x => x.ToTable("ActivityField_Employer_Map"));

        builder.HasData(new Employer
        {
            Id = -1,
            Email = "kadet_2003@list.ru",
            PasswordHash = "0D73C0A5D54B086B544B1A76A121CAE545B6A204F6D85E4CB68A0786991FEC67",
            RegisteredOn = new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
            CompanyName = "Трамплин",
            Description = "Платформа «Трамплин» должна стать экосистемой, где студенты не просто ищут работу, а строят карьеру с нуля: находят менторов, участвуют в карьерных мероприятиях компаний и получают предложения о стажировках, основываясь на своих навыках и активности. Некоторые функциональные и нефункциональные требования описаны заказчиком напрямую, по другим же функциональным и нефункциональным требованиям вам необходимо выработать решения самостоятельно и аргументировать их перед заказчиком"
        });
    }
}