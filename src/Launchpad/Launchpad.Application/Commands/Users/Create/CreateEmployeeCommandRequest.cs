using Launchpad.Shared;
using MediatR;

namespace Launchpad.Application.Commands.Users.Create;

public class CreateEmployeeCommandRequest : IRequest<CreateEmployeeCommandResponse>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required JwtDescriptorDetails JwtDescriptorDetails { get; init; }
}