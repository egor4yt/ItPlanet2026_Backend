using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployerVerificationStatusConfiguration : IEntityTypeConfiguration<EmployerVerificationStatus>
{
    public void Configure(EntityTypeBuilder<EmployerVerificationStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)");

        builder.HasData(
            new EmployerVerificationStatus
            {
                Id = 1,
                Title = "На рассмотрении"
            },
            new EmployerVerificationStatus
            {
                Id = 2,
                Title = "Отказано"
            },
            new EmployerVerificationStatus
            {
                Id = 3,
                Title = "Требуются уточнения"
            },
            new EmployerVerificationStatus
            {
                Id = 4,
                Title = "Подтверждено"
            }
        );
    }
}