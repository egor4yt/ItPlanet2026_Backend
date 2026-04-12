using CSharpFunctionalExtensions;
using Launchpad.Warehouse.Domain.Common;
using Launchpad.Warehouse.Domain.Errors;
using Launchpad.Warehouse.Domain.Events;

namespace Launchpad.Warehouse.Domain.Entities;

public sealed class Skill : EntityWithDomainEvents<Guid>
{
    public Skill()
    {
    }

    public Skill(string title)
    {
        Id = Guid.CreateVersion7();
        Title = title;
        Verified = true;
        
        AddDomainEvent(new SkillUpdated(Id, Title, Verified.Value));
    }

    public Skill(Guid id, string title)
    {
        Id = id;
        Title = title;
        Verified = null;
    }

    public string Title { get; private set; } = null!;
    public bool? Verified { get; private set; }

    public UnitResult<ErrorCollection> Approve(string formatedTitle)
    {
        var errors = new List<Error>();
        if (Verified != null)
        {
            errors.Add(DomainErrors.Skill.AlreadyProcessed);
        }

        if (errors.Count != 0)
        {
            ClearDomainEvents();
            return UnitResult.Failure(new ErrorCollection(errors, ErrorCollectionType.InvalidOperation));
        }
        
        Title = formatedTitle;
        Verified = true;
        
        AddDomainEvent(new Events.SkillUpdated(Id, Title, Verified.Value));
        
        return UnitResult.Success<ErrorCollection>();
    }

    public UnitResult<ErrorCollection> Disapprove()
    {
        var errors = new List<Error>();
        if (Verified != null)
        {
            errors.Add(DomainErrors.Skill.AlreadyProcessed);
        }

        if (errors.Count != 0)
        {
            ClearDomainEvents();
            return UnitResult.Failure(new ErrorCollection(errors, ErrorCollectionType.InvalidOperation));
        }
        
        Verified = false;
        
        AddDomainEvent(new Events.SkillUpdated(Id, Title, Verified.Value));
        
        return UnitResult.Success<ErrorCollection>();
    }
}