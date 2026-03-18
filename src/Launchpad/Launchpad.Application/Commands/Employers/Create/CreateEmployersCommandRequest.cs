using Launchpad.Shared;
using MediatR;

namespace Launchpad.Application.Commands.Employers.Create;

public class CreateEmployersCommandRequest : IRequest<CreateEmployersCommandResponse>
{
    public required string CompanyName { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required JwtDescriptorDetails JwtDescriptorDetails { get; init; }
}