using CSharpFunctionalExtensions;
using Launchpad.Candidates.Domain.Common;
using MediatR;

namespace Launchpad.Candidates.Application.Commands.Candidates.Update;

public class UpdateCandidatesCommandRequest : IRequest<Result<UpdateCandidatesCommandResponse, ErrorCollection>>
{
    public required Guid KeycloakId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; set; }
    public string? Biography { get; set; }
    public DateOnly? Birthdate { get; set; }
}