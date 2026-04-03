using Launchpad.Candidates.Domain.Common;

namespace Launchpad.Candidates.Domain.Errors;

public static class DomainErrors
{
    public static class Candidate
    {
        public static readonly Error InvalidBirthdate = new Error("INVALID_BIRTHDATE", "Invalid birthdate");
        public static readonly Error TooLongBiography = new Error("CANDIDATE_BIOGRAPHY_TOO_LONG", "Biography must not be longer than 2000 characters");
        public static readonly Error MaxSkillsReached = new Error("CANDIDATE_MAX_SKILLS", "Max skills reached");
        public static readonly Error SkillsAlreadyAdded = new Error("SKILL_ALREADY_ADDED", "Skill already added");
    }
}