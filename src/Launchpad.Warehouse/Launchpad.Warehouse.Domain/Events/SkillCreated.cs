using Launchpad.Warehouse.Domain.Common;

namespace Launchpad.Warehouse.Domain.Events;

public record SkillCreated(Guid Id, string Title) : IDomainEvent
{
}