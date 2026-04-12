using Launchpad.Warehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Warehouse.Infrastructure.Persistence.Configuration.Entities;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.Content)
            .HasColumnType("text");

        builder
            .Property(x => x.Type)
            .HasColumnType("text");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.ProcessedAt)
            .HasColumnType("timestamp with time zone");
    }
}