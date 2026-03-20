using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.Create;

public class CreateEmployeesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployeesCommandRequest, CreateEmployeesCommandResponse>
{
    public async Task<CreateEmployeesCommandResponse> Handle(CreateEmployeesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployeesCommandResponse();

        var employeeExists = await applicationDbContext.Employees.AnyAsync(x => x.Email == request.Email, cancellationToken);
        employeeExists = employeeExists || await applicationDbContext.Curators.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (employeeExists) throw new ConflictException("EmployeeAlreadyExists");

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