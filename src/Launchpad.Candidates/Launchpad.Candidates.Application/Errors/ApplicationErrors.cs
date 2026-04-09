using Launchpad.Candidates.Domain.Common;

namespace Launchpad.Candidates.Application.Errors;

public static class ApplicationErrors
{
    public static class UpdateSkillsCandidatesCommand
    {
        public static readonly Error CandidateDoesNotExists = new Error("CANDIDATE_DOES_NOT_EXISTS", "Candidate does not exists");
    }
}