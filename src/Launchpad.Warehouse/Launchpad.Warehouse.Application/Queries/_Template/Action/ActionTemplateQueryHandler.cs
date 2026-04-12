using CSharpFunctionalExtensions;
using Launchpad.Warehouse.Domain.Common;
using Launchpad.Warehouse.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Warehouse.Application.Queries._Template.Action;

public class Action_TemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateQueryRequest, Result<Action_TemplateQueryResponse, ErrorCollection>>
{
    public async Task<Result<Action_TemplateQueryResponse, ErrorCollection>> Handle(Action_TemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateQueryResponse();

        return Result.Success<Action_TemplateQueryResponse, ErrorCollection>(response);
    }
}