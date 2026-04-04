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

    public UnitResult<ErrorCollection> UpdateBirthdate(DateOnly? newBirthdate)
    {
        var minDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-12));
        var maxDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(80));

        if (newBirthdate < minDate || newBirthdate > maxDate)
            return UnitResult.Failure(new ErrorCollection(DomainErrors.Candidate.InvalidBirthdate, ErrorCollectionType.InvalidOperation));

        Birthdate = newBirthdate;

        return UnitResult.Success<ErrorCollection>();
    }

    public UnitResult<ErrorCollection> AddSkill(Skill skill, bool isNewSkill)
    {
        var errors = new List<Error>();
        if (Skills.Any(x => x.Id == skill.Id)) errors.Add(DomainErrors.Candidate.SkillsAlreadyAdded);
        if (Skills.Count >= 20) errors.Add(DomainErrors.Candidate.MaxSkillsReached);

        if (errors.Count > 0)
            return UnitResult.Failure(new ErrorCollection(errors, ErrorCollectionType.InvalidOperation));

        if (isNewSkill)
            AddDomainEvent(new Events.CandidateNewSkillCreated(skill.Id, skill.Title));

        _skills.Add(skill);
        return UnitResult.Success<ErrorCollection>();
    }
}