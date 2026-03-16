using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.Create;

public class CreateEmployeeCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
{
    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployeeCommandResponse();

        var userExists = await applicationDbContext.Employees.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (userExists) throw new ConflictException("EmployeeAlreadyExists");

        var newEmployee = new Employee
        {
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            RegisteredOn = DateTime.UtcNow
        };

        await applicationDbContext.Employees.AddAsync(newEmployee, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.EmployeeId = newEmployee.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(newEmployee));

        return response;
    }
}