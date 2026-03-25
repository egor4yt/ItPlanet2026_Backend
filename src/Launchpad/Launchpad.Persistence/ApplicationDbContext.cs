using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeEducation> EmployeeEducations { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<ActivityFieldGroup> ActivityFieldGroups { get; set; }
    public DbSet<ActivityField> ActivityFields { get; set; }
    public DbSet<Curator> Curators { get; set; }
    public DbSet<EmployerVerificationStatus> EmployerVerificationStatuses { get; set; }
    public DbSet<EmployerVerification> EmployerVerifications { get; set; }
    public DbSet<EmployerVerificationType> EmployerVerificationTypes { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<VacancyType> VacancyTypes { get; set; }
    public DbSet<WorkFormat> WorkFormats { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}