using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Application.Queries.Candidates.GetOne;

public class GetOneCandidatesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneCandidatesQueryRequest, Result<GetOneCandidatesQueryResponse, ErrorCollection>>
{
    public async Task<Result<GetOneCandidatesQueryResponse, ErrorCollection>> Handle(GetOneCandidatesQueryRequest request, CancellationToken cancellationToken)
    {
        var query = applicationDbContext.Candidates.AsQueryable();

        if (request.KeycloakId.HasValue)
            query = query.Where(c => c.KeycloakId == request.KeycloakId);

        if (request.InternalId.HasValue)
            query = query.Where(x => x.Id == request.InternalId);

        var response = await query
            .Select(x => new GetOneCandidatesQueryResponse
            {
                InternalId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                Biography = x.Biography,
                Birthdate = x.Birthdate,
                Skills = x.Skills.Select(s => new GetOneCandidatesQueryResponseSkill
                {
                    Id = s.Id,
                    Title = s.Title
                })
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (response == null)
        {
            var error = new Error("UserNotFound", "User not found");
            var errorCollection = new ErrorCollection(error, ErrorCollectionType.ResourceNotFound);
            return Result.Failure<GetOneCandidatesQueryResponse, ErrorCollection>(errorCollection);
        }

        return Result.Success<GetOneCandidatesQueryResponse, ErrorCollection>(response);
    }
}