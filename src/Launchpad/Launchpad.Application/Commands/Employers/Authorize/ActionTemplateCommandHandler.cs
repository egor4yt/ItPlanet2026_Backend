using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employers.Authorize;

public class AuthorizeEmployersCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<AuthorizeEmployersCommandRequest, AuthorizeEmployersCommandResponse>
{
    public async Task<AuthorizeEmployersCommandResponse> Handle(AuthorizeEmployersCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AuthorizeEmployersCommandResponse();

        var employer = await applicationDbContext.Employers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email
                                      && x.PasswordHash == request.PasswordHash
                , cancellationToken);

        if (employer == null)
            throw new ForbiddenException("Forbidden");

        response.EmployerId = employer.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(employer));

        return response;
    }
}