using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.Update;

public class UpdateEmployeesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateEmployeesCommandRequest, UpdateEmployeesCommandResponse>
{
    public async Task<UpdateEmployeesCommandResponse> Handle(UpdateEmployeesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateEmployeesCommandResponse();

        await applicationDbContext.Employees
            .Where(x => x.Id == request.EmployeeId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsMale, request.IsMale)
                    .SetProperty(p => p.FirstName, request.FirstName)
                    .SetProperty(p => p.LastName, request.LastName)
                    .SetProperty(p => p.MiddleName, request.MiddleName)
                    .SetProperty(p => p.BirthDate, request.BirthDate)
                , cancellationToken);

        return response;
    }
}