using CSharpFunctionalExtensions;
using Launchpad.Warehouse.Domain.Common;
using Launchpad.Warehouse.Infrastructure.Persistence;
using MediatR;

namespace Launchpad.Warehouse.Application.Commands._Template.Action;

public class Action_TemplateCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<Action_TemplateCommandRequest, Result<Action_TemplateCommandResponse, ErrorCollection>>
{
    public async Task<Result<Action_TemplateCommandResponse, ErrorCollection>> Handle(Action_TemplateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new Action_TemplateCommandResponse();

        return Result.Success<Action_TemplateCommandResponse, ErrorCollection>(response);
    }
}