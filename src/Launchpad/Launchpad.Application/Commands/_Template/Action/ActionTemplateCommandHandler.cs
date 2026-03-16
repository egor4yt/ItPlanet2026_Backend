using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Commands._Template.Action;

public class ActionTemplateCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<ActionTemplateCommandRequest, ActionTemplateCommandResponse>
{
    public async Task<ActionTemplateCommandResponse> Handle(ActionTemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ActionTemplateCommandResponse();

        return response;
    }
}