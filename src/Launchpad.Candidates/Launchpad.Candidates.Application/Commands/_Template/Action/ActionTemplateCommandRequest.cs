using CSharpFunctionalExtensions;
using MediatR;

namespace Launchpad.Candidates.Application.Commands._Template.Action;

public class Action_TemplateCommandRequest : IRequest<Result<Action_TemplateCommandResponse>>
{
}