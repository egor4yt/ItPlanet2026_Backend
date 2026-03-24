namespace Launchpad.Domain.Entities;

public sealed class ActivityField
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public int ActivityFieldGroupId { get; init; }

    public ActivityFieldGroup? ActivityFieldGroup { get; set; }
    public ICollection<Employer> Employers { get; init; } = [];
}