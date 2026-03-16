namespace Launchpad.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime RegisteredOn { get; set; }
}