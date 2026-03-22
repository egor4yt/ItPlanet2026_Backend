namespace Launchpad.Domain.Entities;

public sealed class Curator
{
    public long Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string MiddleName { get; init; }
    public required string PasswordHash { get; set; }
    public bool IsAdmin { get; init; }
}