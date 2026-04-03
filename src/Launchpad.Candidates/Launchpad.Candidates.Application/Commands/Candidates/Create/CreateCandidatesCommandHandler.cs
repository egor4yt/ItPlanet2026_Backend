using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Domain.Entities;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Application.Commands.Candidates.Create;

public class CreateCandidatesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateCandidatesCommandRequest, Result<CreateCandidatesCommandResponse, ErrorCollection>>
{
    public async Task<Result<CreateCandidatesCommandResponse, ErrorCollection>> Handle(CreateCandidatesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateCandidatesCommandResponse();

        var existsCandidateId = await applicationDbContext.Candidates
            .Where(x => x.KeycloakId == request.KeycloakId)
            .Select(x => (Guid?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (existsCandidateId.HasValue)
        {
            response.Id = existsCandidateId.Value;
            return Result.Success<CreateCandidatesCommandResponse, ErrorCollection>(response);
        }

        var newCandidate = new Candidate(request.KeycloakId);
        await applicationDbContext.AddAsync(newCandidate, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newCandidate.Id;
        return Result.Success<CreateCandidatesCommandResponse, ErrorCollection>(response);
    }
}