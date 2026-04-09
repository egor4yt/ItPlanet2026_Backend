using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Commands.Candidates.UpdateSkills;

public class UpdateSkillsCandidatesCommandRequest : IRequest<Result<UpdateSkillsCandidatesCommandResponse, ErrorCollection>>
{
    public required Guid KeycloakId { get; init; }
    public required IEnumerable<UpdateSkillsCandidatesCommandRequestSkill> Skills { get; init; }
}

public class UpdateSkillsCandidatesCommandRequestSkill
{
    public Guid? Id { get; init; }
    public required string Title { get; init; }
}