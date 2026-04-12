using CSharpFunctionalExtensions;

namespace Launchpad.Warehouse.Domain.Common;

public abstract class EntityWithDomainEvents<TId> : Entity<TId>, IHasDomainEvents where TId : IComparable<TId>
{
    private readonly IList<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}