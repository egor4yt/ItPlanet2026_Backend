using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Candidates.Application.Commands._Template.Action;

public class Action_TemplateCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateCommandRequest, Result<Action_TemplateCommandResponse, ErrorCollection>>
{
    public async Task<Result<Action_TemplateCommandResponse, ErrorCollection>> Handle(Action_TemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateCommandResponse();

        return Result.Success<Action_TemplateCommandResponse, ErrorCollection>(response);
    }
}