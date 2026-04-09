using CSharpFunctionalExtensions;

namespace Launchpad.Candidates.Domain.Entities;

public sealed class Skill : Entity<Guid>
{
    public Skill()
    {
    }

    public Skill(string title)
    {
        Id = Guid.CreateVersion7();
        Title = title;
        PendingVerification = true;
    }

    public string Title { get; init; } = null!;
    public bool PendingVerification { get; init; }
    public ICollection<Candidate> Candidates { get; init; } = null!;
}