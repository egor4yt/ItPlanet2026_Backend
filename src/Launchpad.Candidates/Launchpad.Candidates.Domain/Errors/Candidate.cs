namespace Launchpad.Candidates.Domain.Errors;

public static class DomainErrors
{
    public static class Candidate
    {
        public static readonly Error InvalidBirthdate = new Error("INVALID_BIRTHDATE");
        public static readonly Error TooLongBiography = new Error("CANDIDATE_BIOGRAPHY_TOO_LONG");
        public static readonly Error MaxSkillsReached = new Error("CANDIDATE_MAX_SKILLS");
        public static readonly Error SkillsAlreadyAdded = new Error("SKILL_ALREADY_ADDED");
    }
}