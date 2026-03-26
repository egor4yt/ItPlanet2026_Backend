using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.HasData(new Color
        {
            Id = 1,
            Title = "Ошибка",
            Code = "#DC2626"
        }, new Color
        {
            Id = 2,
            Title = "Предупреждение",
            Code = "#D97706"
        }, new Color
        {
            Id = 3,
            Title = "Успех",
            Code = "#16A34A"
        }, new Color
        {
            Id = 4,
            Title = "Нейтральный",
            Code = "#6B7280"
        });
    }
}