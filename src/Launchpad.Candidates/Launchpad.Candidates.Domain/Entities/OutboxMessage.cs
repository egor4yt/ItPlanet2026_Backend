using CSharpFunctionalExtensions;

namespace Launchpad.Candidates.Domain.Entities;

public sealed class OutboxMessage : Entity<Guid>
{
    public OutboxMessage()
    {
    }

    public OutboxMessage(string type, string content)
    {
        Id = Guid.CreateVersion7();
        Type = type;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    public string Type { get; } = null!;
    public string Content { get; } = null!;
    public DateTime CreatedAt { get; }
    public DateTime? ProcessedAt { get; private set; }

    public Result CompleteProcessing()
    {
        ProcessedAt = DateTime.UtcNow;
        return Result.Success();
    }
}