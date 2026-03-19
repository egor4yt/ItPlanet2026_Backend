using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class ActivityFieldGroupConfiguration : IEntityTypeConfiguration<ActivityFieldGroup>
{
    public void Configure(EntityTypeBuilder<ActivityFieldGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.HasData(
            new ActivityFieldGroup { Id = -1, Title = "Гостиницы, рестораны, общепит, кейтеринг" },
            new ActivityFieldGroup { Id = -2, Title = "Государственные организации" },
            new ActivityFieldGroup { Id = -3, Title = "Добывающая отрасль" },
            new ActivityFieldGroup { Id = -4, Title = "ЖКХ" },
            new ActivityFieldGroup { Id = -5, Title = "Информационные технологии, системная интеграция, интернет" },
            new ActivityFieldGroup { Id = -6, Title = "Искусство, культура" },
            new ActivityFieldGroup { Id = -7, Title = "Лесная промышленность, деревообработка" },
            new ActivityFieldGroup { Id = -8, Title = "Медицина, фармацевтика, аптеки" },
            new ActivityFieldGroup { Id = -9, Title = "Металлургия, металлообработка" },
            new ActivityFieldGroup { Id = -10, Title = "Нефть и газ" },
            new ActivityFieldGroup { Id = -11, Title = "Образовательные учреждения" },
            new ActivityFieldGroup { Id = -12, Title = "Общественная деятельность, партии, благотворительность, НКО" },
            new ActivityFieldGroup { Id = -13, Title = "Перевозки, логистика, склад, ВЭД" },
            new ActivityFieldGroup { Id = -14, Title = "Продукты питания" },
            new ActivityFieldGroup { Id = -15, Title = "Промышленное оборудование, техника, станки и комплектующие" },
            new ActivityFieldGroup { Id = -16, Title = "Телекоммуникации, связь" },
            new ActivityFieldGroup { Id = -17, Title = "Финансовый сектор" },
            new ActivityFieldGroup { Id = -18, Title = "Электроника, приборостроение, бытовая техника, компьютеры и оргтехника" },
            new ActivityFieldGroup { Id = -19, Title = "Автомобильный бизнес" }
        );
    }
}