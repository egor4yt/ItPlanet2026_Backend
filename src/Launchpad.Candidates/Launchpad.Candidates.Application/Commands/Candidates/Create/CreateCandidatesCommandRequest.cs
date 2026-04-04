using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Commands.Candidates.Create;

public class CreateCandidatesCommandRequest : IRequest<Result<CreateCandidatesCommandResponse, ErrorCollection>>
{
    public required Guid KeycloakId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; set; }
}