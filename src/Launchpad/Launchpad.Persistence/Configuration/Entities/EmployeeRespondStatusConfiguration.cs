using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class EmployeeRespondStatusConfiguration : IEntityTypeConfiguration<EmployeeRespondStatus>
{
    public void Configure(EntityTypeBuilder<EmployeeRespondStatus> builder)
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
            .Property(x => x.Description)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder
            .HasOne(x => x.Color)
            .WithMany(x => x.EmployeeRespondStatuses)
            .HasForeignKey(x => x.ColorId)
            .HasConstraintName("FK_Color_EmployeeRespondStatuses");

        builder.HasData(new EmployeeRespondStatus
        {
            Id = 1,
            Title = "Отказ",
            Description = "Вам отказали",
            ColorId = Domain.Metadata.ColorId.Error
        }, new EmployeeRespondStatus
        {
            Id = 2,
            Title = "Не просмотрено",
            Description = "Отклик",
            ColorId = Domain.Metadata.ColorId.Neutral
        }, new EmployeeRespondStatus
        {
            Id = 3,
            Title = "Приглашение на собеседование",
            Description = "Вас пригласили на собеседование",
            ColorId = Domain.Metadata.ColorId.Success
        });
    }
}