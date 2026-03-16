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
            .FirstOrDefaultAsync(x => x.Email == request.Email
                                      && x.PasswordHash == request.PasswordHash
                , cancellationToken);

        if (employee == null)
            throw new ForbiddenException("Forbidden");

        response.EmployeeId = employee.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(employee));

        return response;
    }
}