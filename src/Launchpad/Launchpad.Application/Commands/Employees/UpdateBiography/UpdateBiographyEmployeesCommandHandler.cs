using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employees.UpdateBiography;

public class UpdateBiographyEmployeesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateBiographyEmployeesCommandRequest, UpdateBiographyEmployeesCommandResponse>
{
    public async Task<UpdateBiographyEmployeesCommandResponse> Handle(UpdateBiographyEmployeesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateBiographyEmployeesCommandResponse();

        await applicationDbContext.Employees
            .Where(x => x.Id == request.EmployeeId)
            .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Biography, request.Biography)
                , cancellationToken);

        return response;
    }
}