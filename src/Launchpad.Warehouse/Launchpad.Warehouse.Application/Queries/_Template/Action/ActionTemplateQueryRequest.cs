using CSharpFunctionalExtensions;
using Launchpad.Warehouse.Domain.Common;
using MediatR;

namespace Launchpad.Warehouse.Application.Queries._Template.Action;

public class Action_TemplateQueryRequest : IRequest<Result<Action_TemplateQueryResponse, ErrorCollection>>
{
}