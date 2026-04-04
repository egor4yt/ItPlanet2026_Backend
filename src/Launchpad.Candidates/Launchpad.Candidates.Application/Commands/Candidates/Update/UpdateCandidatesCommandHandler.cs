using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Application.Commands.Candidates.Update;

public class UpdateCandidatesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateCandidatesCommandRequest, Result<UpdateCandidatesCommandResponse, ErrorCollection>>
{
    public async Task<Result<UpdateCandidatesCommandResponse, ErrorCollection>> Handle(UpdateCandidatesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateCandidatesCommandResponse();

        var candidate = await applicationDbContext.Candidates
            .FirstOrDefaultAsync(cancellationToken);
        if (candidate == null)
        {
            var error = new Error("CandidateExists", "Candidate already exists");
            var errorCollection = new ErrorCollection(error, ErrorCollectionType.ResourceNotFound);
            return Result.Failure<UpdateCandidatesCommandResponse, ErrorCollection>(errorCollection);
        }

        var updateResult = Result.Combine(
            candidate.UpdateNames(request.FirstName, request.LastName, request.MiddleName),
            candidate.UpdateBiography(request.Biography),
            candidate.UpdateBirthdate(request.Birthdate)
        );

        if (updateResult.IsFailure) 
            return Result.Failure<UpdateCandidatesCommandResponse, ErrorCollection>(updateResult.Error);

        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<UpdateCandidatesCommandResponse, ErrorCollection>(response);
    }
}