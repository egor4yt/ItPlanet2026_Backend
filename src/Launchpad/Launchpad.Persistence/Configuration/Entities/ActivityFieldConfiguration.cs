using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Launchpad.Persistence.Configuration.Entities;

public class ActivityFieldConfiguration : IEntityTypeConfiguration<ActivityField>
{
    public void Configure(EntityTypeBuilder<ActivityField> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder
            .HasOne(x => x.ActivityFieldGroup)
            .WithMany(x => x.ActivityFields)
            .HasForeignKey(x => x.ActivityFieldGroupId)
            .HasConstraintName("FK_ActivityFieldGroup_ActivityFields");

        builder.HasData(
            // Автомобильный бизнес (ActivityGroupId = -19)
            new ActivityField { Id = -1901, Title = "Автозапчасти, шины (розничная торговля)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1902, Title = "Автокомпоненты, запчасти (производство)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1903, Title = "Автокомпоненты, запчасти, шины (продвижение, оптовая торговля)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1904, Title = "Легковые, грузовые автомобили, мототехника, автобусы, троллейбусы (продвижение, оптовая торговля)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1905, Title = "Легковые, грузовые автомобили, мототехника, автобусы, троллейбусы (производство)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1906, Title = "Розничная торговля автомобилями (дилерский центр)", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1907, Title = "Техническое обслуживание, ремонт автомобилей", ActivityFieldGroupId = -19 },
            new ActivityField { Id = -1908, Title = "Финансовые услуги (кэптивные банки и лизинговые компании)", ActivityFieldGroupId = -19 },

// Гостиницы, рестораны, общепит, кейтеринг (ActivityGroupId = -1)
            new ActivityField { Id = -101, Title = "Гостиница", ActivityFieldGroupId = -1 },
            new ActivityField { Id = -102, Title = "Кейтеринг (выездное обслуживание)", ActivityFieldGroupId = -1 },
            new ActivityField { Id = -103, Title = "Ресторан, общественное питание, фаст-фуд", ActivityFieldGroupId = -1 },

// Государственные организации (ActivityGroupId = -2)
            new ActivityField { Id = -201, Title = "Государственные организации", ActivityFieldGroupId = -2 },

// Добывающая отрасль (ActivityGroupId = -3)
            new ActivityField { Id = -301, Title = "Добыча и обогащение минерального сырья (соль, сера, глинозем), разработка карьеров (песок, глина, камень), добыча торфа", ActivityFieldGroupId = -3 },
            new ActivityField { Id = -302, Title = "Добыча и обогащение руд черных, цветных, драгоценных, благородных, редких металлов", ActivityFieldGroupId = -3 },
            new ActivityField { Id = -303, Title = "Добыча и обогащение угля", ActivityFieldGroupId = -3 },
            new ActivityField { Id = -304, Title = "Инженерно-изыскательские, гидрогеологические, геологоразведочные работы", ActivityFieldGroupId = -3 },

// ЖКХ (ActivityGroupId = -4)
            new ActivityField { Id = -401, Title = "Благоустройство и уборка территорий и зданий", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -402, Title = "Вентиляция и кондиционирование (монтаж, сервис, ремонт)", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -403, Title = "Водоснабжение и канализация", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -404, Title = "Лифтовое хозяйство (монтаж, сервис, ремонт)", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -405, Title = "Обеспечение пожарной безопасности, молниезащиты", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -406, Title = "Ремонт зданий и сооружений", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -407, Title = "Слаботочные сети (монтаж, сервис, ремонт)", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -408, Title = "Утилизация бытовых отходов", ActivityFieldGroupId = -4 },
            new ActivityField { Id = -409, Title = "Энергоснабжение", ActivityFieldGroupId = -4 },

// Информационные технологии, системная интеграция, интернет (ActivityGroupId = -5)
            new ActivityField { Id = -501, Title = "Интернет-компания (поисковики, платежные системы, соц.сети, информационно-познавательные и развлекательные ресурсы, продвижение сайтов и прочее)", ActivityFieldGroupId = -5 },
            new ActivityField { Id = -502, Title = "Интернет-провайдер", ActivityFieldGroupId = -5 },
            new ActivityField { Id = -503, Title = "Разработка программного обеспечения", ActivityFieldGroupId = -5 },
            new ActivityField { Id = -504, Title = "Системная интеграция, автоматизация технологических и бизнес-процессов предприятия, ИТ-консалтинг", ActivityFieldGroupId = -5 },

// Искусство, культура (ActivityGroupId = -6)
            new ActivityField { Id = -601, Title = "Архив, библиотека, искусствоведение", ActivityFieldGroupId = -6 },
            new ActivityField { Id = -602, Title = "Ботанический сад, зоопарк, заповедник", ActivityFieldGroupId = -6 },
            new ActivityField { Id = -603, Title = "Музей, галерея, театр", ActivityFieldGroupId = -6 },

// Лесная промышленность, деревообработка (ActivityGroupId = -7)
            new ActivityField { Id = -701, Title = "Деревообработка (производство)", ActivityFieldGroupId = -7 },
            new ActivityField { Id = -702, Title = "Продукция деревообработки (продвижение, оптовая торговля)", ActivityFieldGroupId = -7 },
            new ActivityField { Id = -703, Title = "Столярно-строительные изделия (продвижение, оптовая торговля)", ActivityFieldGroupId = -7 },
            new ActivityField { Id = -704, Title = "Столярно-строительные изделия (производство)", ActivityFieldGroupId = -7 },
            new ActivityField { Id = -705, Title = "Целлюлозно-бумажная продукция (продвижение, оптовая торговля)", ActivityFieldGroupId = -7 },
            new ActivityField { Id = -706, Title = "Целлюлозно-бумажное производство", ActivityFieldGroupId = -7 },

            // Медицина, фармацевтика, аптеки (ActivityGroupId = -8)
            new ActivityField { Id = -801, Title = "Аптека, оптика", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -802, Title = "Ветеринарная аптека", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -803, Title = "Ветеринарная деятельность", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -804, Title = "Клинические исследования", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -805, Title = "Лаборатория, исследовательский центр", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -806, Title = "Лечебно-профилактические учреждения", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -807, Title = "Учреждение соц.помощи и защиты", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -808, Title = "Фармацевтическая продукция (продвижение, оптовая торговля)", ActivityFieldGroupId = -8 },
            new ActivityField { Id = -809, Title = "Фармацевтическая продукция (производство)", ActivityFieldGroupId = -8 },

// Металлургия, металлообработка (ActivityGroupId = -9)
            new ActivityField { Id = -901, Title = "Драгоценные, благородные и редкие металлы (продвижение, оптовая торговля)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -902, Title = "Драгоценные, благородные и редкие металлы (производство)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -903, Title = "Металлические изделия, металлоконструкции (продвижение, оптовая торговля)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -904, Title = "Металлические изделия, металлоконструкции (производство)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -905, Title = "Продукция цветной металлургии (продвижение, оптовая торговля)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -906, Title = "Продукция черной металлургии (продвижение, оптовая торговля)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -907, Title = "Цветная металлургия (выплавка, металлопрокат)", ActivityFieldGroupId = -9 },
            new ActivityField { Id = -908, Title = "Черная металлургия (производство чугуна, стали, проката)", ActivityFieldGroupId = -9 },

// Нефть и газ (ActivityGroupId = -10)
            new ActivityField { Id = -1001, Title = "ГСМ, топливо (продвижение, оптовая торговля)", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1002, Title = "ГСМ, топливо (розничная торговля)", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1003, Title = "Добыча газа", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1004, Title = "Добыча нефти", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1005, Title = "Нефтепереработка, нефтехимия (производство)", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1006, Title = "Нефтехимия (продвижение, оптовая торговля)", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1007, Title = "Переработка газа", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1008, Title = "Транспортировка, хранение газа", ActivityFieldGroupId = -10 },
            new ActivityField { Id = -1009, Title = "Транспортировка, хранение нефти", ActivityFieldGroupId = -10 },

// Образовательные учреждения (ActivityGroupId = -11)
            new ActivityField { Id = -1101, Title = "Автошкола", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1102, Title = "Бизнес-образование", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1103, Title = "Вуз, ссуз колледж, ПТУ", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1104, Title = "Интернат, детский дом", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1105, Title = "Научно-исследовательская, научная, академическая деятельность", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1106, Title = "Обучение иностранным языкам", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1107, Title = "Обучение искусствам (рисование, пение, танцы, фото)", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1108, Title = "Повышение квалификации, переквалификация", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1109, Title = "Спортивное обучение", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1110, Title = "Тренинговые компании", ActivityFieldGroupId = -11 },
            new ActivityField { Id = -1111, Title = "Школа, детский сад", ActivityFieldGroupId = -11 },

// Общественная деятельность, партии, благотворительность, НКО (ActivityGroupId = -12)
            new ActivityField { Id = -1201, Title = "Ассоциация в сфере культуры, искусства", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1202, Title = "Благотворительная организация", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1203, Title = "Общественная, политическая организация", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1204, Title = "Профессиональная, предпринимательская организация", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1205, Title = "Религиозная организация", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1206, Title = "Спортивная федерация", ActivityFieldGroupId = -12 },
            new ActivityField { Id = -1207, Title = "Фонд, грантодатель", ActivityFieldGroupId = -12 },

// Перевозки, логистика, склад, ВЭД (ActivityGroupId = -13)
            new ActivityField { Id = -1301, Title = "Авиаперевозки", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1302, Title = "Автомобильные перевозки", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1303, Title = "ВЭД, таможенное оформление", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1304, Title = "Железнодорожные перевозки", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1305, Title = "Курьерская, почтовая доставка", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1306, Title = "Морские, речные перевозки", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1307, Title = "Складские услуги", ActivityFieldGroupId = -13 },
            new ActivityField { Id = -1308, Title = "Транспортно-логистические комплексы, порты (воздушный, водный, железнодорожный)", ActivityFieldGroupId = -13 },

// Продукты питания (ActivityGroupId = -14)
            new ActivityField { Id = -1401, Title = "Алкогольные напитки (продвижение, оптовая торговля)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1402, Title = "Безалкогольные напитки (продвижение, оптовая торговля)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1403, Title = "Безалкогольные напитки (производство)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1404, Title = "Вино (производство)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1405, Title = "Детское питание (продвижение, оптовая торговля)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1406, Title = "Детское питание (производство)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1407, Title = "Кондитерские изделия (продвижение, оптовая торговля)", ActivityFieldGroupId = -14 },
            new ActivityField { Id = -1408, Title = "Кондитерские изделия (производство)", ActivityFieldGroupId = -14 },

// Промышленное оборудование, техника, станки и комплектующие (ActivityGroupId = -15)
            new ActivityField { Id = -1501, Title = "Оборудование и станки для добывающей, энергетической, нефтегазовой и химической отрасли (монтаж, сервис, ремонт)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1502, Title = "Оборудование и станки для добывающей, энергетической, нефтегазовой и химической отрасли (продвижение, оптовая торговля)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1503, Title = "Оборудование и станки для добывающей, энергетической, нефтегазовой и химической отрасли (производство)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1504, Title = "Оборудование и станки для металлургии и металлообработки (монтаж, сервис, ремонт)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1505, Title = "Оборудование и станки для металлургии и металлообработки (продвижение, оптовая торговля)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1506, Title = "Оборудование и станки для металлургии и металлообработки (производство)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1507, Title = "Оборудование и станки для отраслей легкой промышленности (монтаж, сервис, ремонт)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1508, Title = "Оборудование и станки для отраслей легкой промышленности (продвижение, оптовая торговля)", ActivityFieldGroupId = -15 },
            new ActivityField { Id = -1509, Title = "Оборудование и станки для отраслей легкой промышленности (производство)", ActivityFieldGroupId = -15 },

// Телекоммуникации, связь (ActivityGroupId = -16)
            new ActivityField { Id = -1601, Title = "Мобильная связь", ActivityFieldGroupId = -16 },
            new ActivityField { Id = -1602, Title = "Оптоволоконная связь", ActivityFieldGroupId = -16 },
            new ActivityField { Id = -1603, Title = "Спутниковая связь", ActivityFieldGroupId = -16 },
            new ActivityField { Id = -1604, Title = "Фиксированная связь", ActivityFieldGroupId = -16 },

// Финансовый сектор (ActivityGroupId = -17)
            new ActivityField { Id = -1701, Title = "Аудит, управленческий учет, финансово-юридический консалтинг", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1702, Title = "Банк", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1703, Title = "Коллекторская деятельность", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1704, Title = "Лизинговые компании", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1705, Title = "НПФ", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1706, Title = "Страхование, перестрахование", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1707, Title = "Управляющая, инвестиционная компания (управление активами)", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1708, Title = "Услуги по ведению бухгалтерского и налогового учета, расчет заработной платы", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1709, Title = "Факторинговые компании", ActivityFieldGroupId = -17 },
            new ActivityField { Id = -1710, Title = "Финансово-кредитное посредничество (биржа, брокерская деятельность, выпуск и обслуживание карт, оценка рисков, обменные пункты, агентства по кредитованию, инкассация, ломбард, платежные системы)", ActivityFieldGroupId = -17 },

// Электроника, приборостроение, бытовая техника, компьютеры и оргтехника (ActivityGroupId = -18)
            new ActivityField { Id = -1801, Title = "Бытовая техника, электроника, климатическое оборудование (монтаж, сервис, ремонт)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1802, Title = "Бытовая техника, электроника, климатическое оборудование (продвижение, оптовая торговля)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1803, Title = "Бытовая техника, электроника, климатическое оборудование (производство)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1804, Title = "Промышленное, бытовое электрооборудование и электротехника (монтаж, сервис, ремонт)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1805, Title = "Промышленное, бытовое электрооборудование и электротехника (продвижение, оптовая торговля)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1806, Title = "Промышленное, бытовое электрооборудование и электротехника (производство)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1807, Title = "Электронно-вычислительная, оптическая, контрольно-измерительная техника, радиоэлектроника, автоматика (монтаж, сервис, ремонт)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1808, Title = "Электронно-вычислительная, оптическая, контрольно-измерительная техника, радиоэлектроника, автоматика (продвижение, оптовая торговля)", ActivityFieldGroupId = -18 },
            new ActivityField { Id = -1809, Title = "Электронно-вычислительная, оптическая, контрольно-измерительная техника, радиоэлектроника, автоматика (производство)", ActivityFieldGroupId = -18 }
        );
    }
}