using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Commands._Template.Action;

public class Action_TemplateCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateCommandRequest, Action_TemplateCommandResponse>
{
    public async Task<Action_TemplateCommandResponse> Handle(Action_TemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateCommandResponse();

        return response;
    }
}