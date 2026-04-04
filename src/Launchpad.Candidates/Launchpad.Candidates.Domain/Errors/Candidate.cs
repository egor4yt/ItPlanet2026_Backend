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
        public static readonly Error InvalidFirstName = new Error("INVALID_FRIST_NAME", "First name must not be empty and be less than 128 characters");
        public static readonly Error InvalidLastName = new Error("INVALID_LAST_NAME", "Last name must not be empty and be less than 128 characters");
        public static readonly Error InvalidMiddleName = new Error("INVALID_MIDDLE_NAME", "Middle name must be less than 128 characters");
    }
}