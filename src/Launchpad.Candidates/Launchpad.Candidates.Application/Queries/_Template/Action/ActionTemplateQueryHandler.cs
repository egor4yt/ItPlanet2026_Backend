using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Candidates.Application.Queries._Template.Action;

public class Action_TemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateQueryRequest, Result<Action_TemplateQueryResponse, ErrorCollection>>
{
    public async Task<Result<Action_TemplateQueryResponse, ErrorCollection>> Handle(Action_TemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateQueryResponse();

        return Result.Success<Action_TemplateQueryResponse, ErrorCollection>(response);
    }
}