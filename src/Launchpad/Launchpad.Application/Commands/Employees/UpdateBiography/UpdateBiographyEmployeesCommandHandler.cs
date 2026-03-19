using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.UpdateBiography;

public class UpdateBiographyEmployeesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateBiographyEmployeesCommandRequest>
{
    public async Task Handle(UpdateBiographyEmployeesCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.Employees
            .Where(x => x.Id == request.EmployeeId)
            .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Biography, request.Biography)
                , cancellationToken);
    }
}