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
                Title = "WebAssembly",
                IsSystemTag = true
            }, new Skill
            {
                Id = -2,
                Title = "Dart",
                IsSystemTag = true
            }, new Skill
            {
                Id = -3,
                Title = "Scala",
                IsSystemTag = true
            }, new Skill
            {
                Id = -4,
                Title = "HTML",
                IsSystemTag = true
            }, new Skill
            {
                Id = -5,
                Title = "Apache Airflow",
                IsSystemTag = true
            }, new Skill
            {
                Id = -6,
                Title = "Solidity",
                IsSystemTag = true
            }, new Skill
            {
                Id = -7,
                Title = "Vite",
                IsSystemTag = true
            }, new Skill
            {
                Id = -8,
                Title = ".NET Core / .NET 5+",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -9,
                Title = "C#",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -10,
                Title = "Java",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -11,
                Title = "Python",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -12,
                Title = "JavaScript",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -13,
                Title = "TypeScript",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -14,
                Title = "React",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -15,
                Title = "Angular",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -16,
                Title = "Vue.js",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -17,
                Title = "Node.js",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -18,
                Title = "Docker",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -19,
                Title = "Kubernetes",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -20,
                Title = "PostgreSQL",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -21,
                Title = "MS SQL Server",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -22,
                Title = "MySQL",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -23,
                Title = "MongoDB",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -24,
                Title = "Redis",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -25,
                Title = "RabbitMQ",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -26,
                Title = "Apache Kafka",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -27,
                Title = "AWS",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -28,
                Title = "Azure",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -29,
                Title = "Google Cloud Platform",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -30,
                Title = "Git",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -31,
                Title = "CI/CD",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -32,
                Title = "Microservices",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -33,
                Title = "REST API",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -34,
                Title = "GraphQL",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -35,
                Title = "gRPC",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -36,
                Title = "Unit Testing",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -37,
                Title = "TDD",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -38,
                Title = "DDD",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -39,
                Title = "SOLID",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -40,
                Title = "Design Patterns",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -41,
                Title = "Spring Boot",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -42,
                Title = "Go (Golang)",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -43,
                Title = "Rust",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -44,
                Title = "C++",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -45,
                Title = "PHP",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -46,
                Title = "Laravel",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -47,
                Title = "Ruby on Rails",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -48,
                Title = "Django",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -49,
                Title = "FastAPI",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -50,
                Title = "Flutter",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -51,
                Title = "React Native",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -52,
                Title = "Swift",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -53,
                Title = "Kotlin",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -54,
                Title = "Android SDK",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -55,
                Title = "iOS SDK",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -56,
                Title = "Terraform",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -57,
                Title = "Ansible",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -58,
                Title = "Elasticsearch",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -59,
                Title = "Prometheus",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -60,
                Title = "Grafana",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -61,
                Title = "Next.js",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -62,
                Title = "Nuxt.js",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -63,
                Title = "Tailwind CSS",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -64,
                Title = "Redux",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -65,
                Title = "WebSockets",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -66,
                Title = "Firebase",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -67,
                Title = "PyTorch",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -68,
                Title = "TensorFlow",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -69,
                Title = "Machine Learning",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -70,
                Title = "Data Science",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -71,
                Title = "Pandas",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -72,
                Title = "Big Data",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -73,
                Title = "Spark",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -74,
                Title = "Hadoop",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -75,
                Title = "Cybersecurity",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -76,
                Title = "Penetration Testing",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -77,
                Title = "Linux",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -78,
                Title = "Bash",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -79,
                Title = "PowerShell",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -80,
                Title = "Unity",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -81,
                Title = "Unreal Engine",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -82,
                Title = "QA Automation",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -83,
                Title = "Selenium",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -84,
                Title = "Playwright",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -85,
                Title = "Cypress",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -86,
                Title = "Jira",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -87,
                Title = "Agile",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -88,
                Title = "Scrum",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -89,
                Title = "Kanban",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -90,
                Title = "Product Management",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -91,
                Title = "Team Management",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -92,
                Title = "Soft Skills",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -93,
                Title = "English (Intermediate+)",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -94,
                Title = "NoSQL",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -95,
                Title = "SQL",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -96,
                Title = "OpenAPI / Swagger",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -97,
                Title = "Cassandra",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -98,
                Title = "ClickHouse",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -99,
                Title = "Svelte",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -100,
                Title = "Blazor",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -101,
                Title = "Entity Framework Core",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -102,
                Title = "Dapper",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -103,
                Title = "Nginx",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -104,
                Title = "Helm",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -105,
                Title = "Figma",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -106,
                Title = "UI/UX Design",
                IsSystemTag = true
            },
            new Skill
            {
                Id = -107,
                Title = "Highload",
                IsSystemTag = true
            });
    }
}