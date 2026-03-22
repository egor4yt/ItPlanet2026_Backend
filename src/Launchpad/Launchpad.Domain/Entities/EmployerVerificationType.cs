namespace Launchpad.Domain.Entities;

public sealed class EmployerVerificationType
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string HtmlDescription { get; init; }

    public ICollection<EmployerVerification> EmployerVerifications { get; init; } = [];
}