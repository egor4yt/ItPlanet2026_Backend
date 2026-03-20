using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.Authorize;

public class AuthorizeEmployeeCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<AuthorizeEmployeeCommandRequest, AuthorizeEmployeeCommandResponse>
{
    public async Task<AuthorizeEmployeeCommandResponse> Handle(AuthorizeEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new AuthorizeEmployeeCommandResponse();

        var employee = await applicationDbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email
                                      && x.PasswordHash == request.PasswordHash
                , cancellationToken);

        if (employee == null)
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

        response.ProfileId = employee.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(employee));

        return response;
    }
}