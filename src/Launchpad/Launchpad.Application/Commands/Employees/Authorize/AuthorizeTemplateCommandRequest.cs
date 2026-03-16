using Launchpad.Shared;
using MediatR;

namespace Launchpad.Application.Commands.Employees.Authorize;

public class AuthorizeEmployeeCommandRequest : IRequest<AuthorizeEmployeeCommandResponse>
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required JwtDescriptorDetails JwtDescriptorDetails { get; init; }
}