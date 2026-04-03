using CSharpFunctionalExtensions;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Candidates.Application.Queries._Template.Action;

public class Action_TemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateQueryRequest, Result<Action_TemplateQueryResponse>>
{
    public async Task<Result<Action_TemplateQueryResponse>> Handle(Action_TemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateQueryResponse();

        return Result.Success(response);
    }
}