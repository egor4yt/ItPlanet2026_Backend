
using Launchpad.Warehouse.Domain.Common;

namespace Launchpad.Warehouse.Domain.Events;

public record SkillUpdated(Guid Id, string FormatedTitle, bool Approved) : IDomainEvent
{
}