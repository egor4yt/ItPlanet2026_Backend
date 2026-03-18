using Launchpad.Shared;
using MediatR;

namespace Launchpad.Application.Commands.Employers.Authorize;

public class AuthorizeEmployersCommandRequest : IRequest<AuthorizeEmployersCommandResponse>
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required JwtDescriptorDetails JwtDescriptorDetails { get; init; }
}