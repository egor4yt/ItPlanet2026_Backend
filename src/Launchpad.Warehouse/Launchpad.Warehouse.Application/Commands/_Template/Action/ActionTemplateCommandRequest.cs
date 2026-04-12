using CSharpFunctionalExtensions;
using Launchpad.Warehouse.Domain.Common;
using MediatR;

namespace Launchpad.Warehouse.Application.Commands._Template.Action;

public class Action_TemplateCommandRequest : IRequest<Result<Action_TemplateCommandResponse, ErrorCollection>>
{
}