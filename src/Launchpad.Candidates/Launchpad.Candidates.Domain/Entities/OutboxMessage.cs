namespace Launchpad.Candidates.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? ProcessedAt { get; init; }
}