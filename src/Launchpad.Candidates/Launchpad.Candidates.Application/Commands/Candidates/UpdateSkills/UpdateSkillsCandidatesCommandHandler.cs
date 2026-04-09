using CSharpFunctionalExtensions;
using Launchpad.Candidates.Application.Errors;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Domain.Entities;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Application.Commands.Candidates.UpdateSkills;

public class UpdateSkillsCandidatesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSkillsCandidatesCommandRequest, Result<UpdateSkillsCandidatesCommandResponse, ErrorCollection>>
{
    public async Task<Result<UpdateSkillsCandidatesCommandResponse, ErrorCollection>> Handle(UpdateSkillsCandidatesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateSkillsCandidatesCommandResponse();

        var candidate = await applicationDbContext.Candidates
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.KeycloakId == request.KeycloakId, cancellationToken);

        if (candidate == null)
            return Result.Failure<UpdateSkillsCandidatesCommandResponse, ErrorCollection>(new ErrorCollection(ApplicationErrors.UpdateSkillsCandidatesCommand.CandidateDoesNotExists, ErrorCollectionType.ResourceNotFound));

        var existsSkillsIds = request.Skills
            .Where(x => x.Id.HasValue)
            .Select(s => s.Id);

        var existsSkills = await applicationDbContext.Skills
            .AsNoTracking()
            .Where(x => existsSkillsIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var newSkills = request.Skills
            .Where(x => x.Id.HasValue == false)
            .Select(skill => new Skill(skill.Title)).ToList();

        var candidateSkills = existsSkills
            .Select(x => (Skill: x, IsNewSkill: false))
            .Concat(newSkills.Select(x => (Skill: x, IsNewSkill: true)))
            .ToList();
        var updateResult = candidate.UpdateSkills(candidateSkills);

        if (updateResult.IsFailure)
            return Result.Failure<UpdateSkillsCandidatesCommandResponse, ErrorCollection>(updateResult.Error);

        await applicationDbContext.Skills.AddRangeAsync(newSkills, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<UpdateSkillsCandidatesCommandResponse, ErrorCollection>(response);
    }
}