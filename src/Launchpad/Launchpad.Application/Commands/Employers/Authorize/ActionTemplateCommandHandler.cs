using Launchpad.Application.Exceptions;
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
        {
            var curator = await applicationDbContext.Curators
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email
                                          && x.PasswordHash == request.PasswordHash
                    , cancellationToken);

            if (curator != null)
            {
                response.ProfileId = curator.Id;
                response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(curator));

                return response;
            }
            
            throw new ForbiddenException("Forbidden");
        }

        response.ProfileId = employer.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(employer));

        return response;
    }
}