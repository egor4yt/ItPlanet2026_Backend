namespace Launchpad.Domain.Entities;

public sealed class ActivityFieldGroup
{
    public int Id { get; init; }
    public required string Title { get; init; }

    public ICollection<ActivityField> ActivityFields { get; init; } = [];
}