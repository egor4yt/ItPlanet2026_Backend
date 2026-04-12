using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Domain.Errors;

namespace Launchpad.Candidates.Domain.Entities;

public sealed class Candidate : EntityWithDomainEvents<Guid>
{
    private readonly IList<Skill> _skills = [];

    private Candidate()
    {
    }

    public Candidate(Guid keycloakId, string firstName, string lastName, string? middleName)
    {
        if (keycloakId == Guid.Empty)
            throw new ArgumentException("KeycloakId cannot be empty", nameof(keycloakId));

        Id = Guid.CreateVersion7();
        KeycloakId = keycloakId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }

    public Guid KeycloakId { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public string? Biography { get; private set; }
    public DateOnly? Birthdate { get; private set; }

    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    public UnitResult<ErrorCollection> UpdateBiography(string? newBiography)
    {
        if (newBiography?.Length > 2000)
            return UnitResult.Failure(new ErrorCollection(DomainErrors.Candidate.TooLongBiography, ErrorCollectionType.InvalidOperation));

        Biography = newBiography;

        return UnitResult.Success<ErrorCollection>();
    }

    public UnitResult<ErrorCollection> UpdateNames(string firstName, string lastName, string? middleName)
    {
        var errors = new List<Error>();
        if (firstName.Length > 128)
            errors.Add(DomainErrors.Candidate.InvalidFirstName);

        if (lastName.Length > 128)
            errors.Add(DomainErrors.Candidate.InvalidLastName);

        if (middleName is { Length: > 128 or 0 })
            errors.Add(DomainErrors.Candidate.InvalidMiddleName);

        if (errors.Count > 0)
        {
            var errorCollection = new ErrorCollection(errors, ErrorCollectionType.InvalidOperation);
            UnitResult.Failure(errorCollection);
        }

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        return UnitResult.Success<ErrorCollection>();
    }

    public UnitResult<ErrorCollection> UpdateBirthdate(DateOnly? newBirthdate)
    {
        var maxDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-12));
        var minDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-100));

        if (newBirthdate < minDate || newBirthdate > maxDate)
            return UnitResult.Failure(new ErrorCollection(DomainErrors.Candidate.InvalidBirthdate, ErrorCollectionType.InvalidOperation));

        Birthdate = newBirthdate;

        return UnitResult.Success<ErrorCollection>();
    }

    public UnitResult<ErrorCollection> UpdateSkills(List<(Skill skill, bool isNewSkill)> skills)
    {
        var errors = new List<Error>();
        if (skills.Count >= 20) errors.Add(DomainErrors.Candidate.MaxSkillsReached);

        foreach (var (skill, isNewSkill) in skills)
        {
            if (Skills.Any(x => x.Id == skill.Id)) errors.Add(DomainErrors.Candidate.SkillsAlreadyAdded);

            if (isNewSkill)
                AddDomainEvent(new Events.SkillCreated(skill.Id, skill.Title));

            _skills.Add(skill);
        }

        if (errors.Count > 0)
        {
            ClearDomainEvents();
            return UnitResult.Failure(new ErrorCollection(errors, ErrorCollectionType.InvalidOperation));
        }

        return UnitResult.Success<ErrorCollection>();
    }
}