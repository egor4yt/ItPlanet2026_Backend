using CSharpFunctionalExtensions;

namespace Launchpad.Candidates.Domain.Entities;

public sealed class Skill : Entity<Guid>
{
    public required string Title { get; init; }
    public required bool PendingVerification { get; init; }
}