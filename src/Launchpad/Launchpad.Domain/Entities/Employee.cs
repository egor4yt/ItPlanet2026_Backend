namespace Launchpad.Domain.Entities;

public sealed class Employee
{
    public long Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PasswordHash { get; set; }
    public DateTime RegisteredOn { get; init; }

    public string? MiddleName { get; init; }
    public bool? IsMale { get; init; }
    public string? Biography { get; init; }
    public DateOnly? BirthDate { get; init; }

    public ICollection<EmployeeEducation> EmployeeEducations { get; init; } = [];
    public ICollection<EmployeeProject> EmployeeProjects { get; init; } = [];
    public ICollection<Skill> Skills { get; set; } = [];
    public ICollection<EmployeeRespond> EmployeeResponds { get; init; } = [];
}