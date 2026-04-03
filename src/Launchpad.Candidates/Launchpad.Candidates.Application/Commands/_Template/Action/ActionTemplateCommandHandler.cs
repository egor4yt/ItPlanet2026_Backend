using CSharpFunctionalExtensions;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Candidates.Application.Commands._Template.Action;

public class Action_TemplateCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateCommandRequest, Result<Action_TemplateCommandResponse>>
{
    public async Task<Result<Action_TemplateCommandResponse>> Handle(Action_TemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateCommandResponse();

        return Result.Success(response);
    }
}