using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Queries._Template.Action;

public class Action_TemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateQueryRequest, Action_TemplateQueryResponse>
{
    public async Task<Action_TemplateQueryResponse> Handle(Action_TemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateQueryResponse();


        return response;
    }
}