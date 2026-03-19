using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployerVerificationConfiguration : IEntityTypeConfiguration<EmployerVerification>
{
    public void Configure(EntityTypeBuilder<EmployerVerification> builder)
    {
        builder
            .Property(x => x.RequestMessage)
            .HasColumnType("text");

        builder
            .Property(x => x.ResponseMessage)
            .HasColumnType("text");

        builder
            .Property(x => x.ChangedOn)
            .HasColumnType("timestamp with time zone");

        builder
            .HasOne(x => x.Employer)
            .WithOne(x => x.Verification)
            .HasForeignKey<EmployerVerification>(x => x.EmployerId)
            .HasConstraintName("FK_Employer_Verification");

        builder
            .HasOne(x => x.EmployerVerificationStatus)
            .WithMany(x => x.EmployerVerifications)
            .HasForeignKey(x => x.StatusId)
            .HasConstraintName("FK_EmployerVerificationStatus_EmployerVerifications");
    }
}