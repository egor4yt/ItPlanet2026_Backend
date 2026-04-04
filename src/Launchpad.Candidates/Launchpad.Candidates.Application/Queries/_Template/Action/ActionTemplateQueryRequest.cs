using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Queries._Template.Action;

public class Action_TemplateQueryRequest : IRequest<Result<Action_TemplateQueryResponse, ErrorCollection>>
{
}