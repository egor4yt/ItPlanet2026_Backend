namespace Launchpad.Domain.Entities;

public sealed class EmployerVerificationStatus
{
    public int Id { get; init; }
    public required string Title { get; init; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<EmployerVerification> EmployerVerifications { get; } = [];
}