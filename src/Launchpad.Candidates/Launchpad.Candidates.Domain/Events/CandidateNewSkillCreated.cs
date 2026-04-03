using Launchpad.Candidates.Domain.Common;

namespace Launchpad.Candidates.Domain.Events;

public record CandidateNewSkillCreated(Guid Id, string Title) : IDomainEvent
{
}