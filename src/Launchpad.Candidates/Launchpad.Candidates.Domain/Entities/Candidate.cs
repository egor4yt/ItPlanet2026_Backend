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

    public Candidate(Guid keycloakId)
    {
        if (keycloakId == Guid.Empty)
            throw new ArgumentException("KeycloakId cannot be empty", nameof(keycloakId));

        Id = Guid.CreateVersion7();
        KeycloakId = keycloakId;
    }

    public required Guid KeycloakId { get; init; }
    public string? Biography { get; private set; }
    public DateOnly? Birthdate { get; private set; }

    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    public Result UpdateBiography(string? newBiography)
    {
        if (newBiography?.Length > 2000)
            return Result.Failure(DomainErrors.Candidate.InvalidBirthdate.Code);

        Biography = newBiography;

        return Result.Success();
    }

    public Result UpdateBirthdate(DateOnly? newBirthdate)
    {
        var minDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-12));
        var maxDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(80));

        if (newBirthdate < minDate || newBirthdate > maxDate)
            return Result.Failure(DomainErrors.Candidate.TooLongBiography.Code);

        Birthdate = newBirthdate;

        return Result.Success();
    }

    public Result AddSkill(Skill skill, bool isNewSkill)
    {
        if (Skills.Any(x => x.Id == skill.Id)) return Result.Failure(DomainErrors.Candidate.SkillsAlreadyAdded.Code);
        if (Skills.Count >= 20) return Result.Failure(DomainErrors.Candidate.MaxSkillsReached.Code);

        if (isNewSkill)
            AddDomainEvent(new Events.CandidateNewSkillCreated(skill.Id, skill.Title));

        _skills.Add(skill);
        return Result.Success();
    }
}