using Launchpad.Candidates.Domain.Common;

namespace Launchpad.Candidates.Domain.Events;

public record SkillCreated(Guid Id, string Title) : IDomainEvent
{
}