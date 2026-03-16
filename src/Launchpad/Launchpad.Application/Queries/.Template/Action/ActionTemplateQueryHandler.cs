using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Queries.Template.Action;

public class ActionTemplateQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<ActionTemplateQueryRequest, ActionTemplateQueryResponse>
{
    public async Task<ActionTemplateQueryResponse> Handle(ActionTemplateQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new ActionTemplateQueryResponse();

        
        
        return response;
    }
}