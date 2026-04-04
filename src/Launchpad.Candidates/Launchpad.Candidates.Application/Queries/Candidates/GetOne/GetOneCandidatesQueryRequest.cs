using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Queries.Candidates.GetOne;

public class GetOneCandidatesQueryRequest : IRequest<Result<GetOneCandidatesQueryResponse, ErrorCollection>>
{
    public Guid? KeycloakId { get; init; }
    public Guid? InternalId { get; init; }
}