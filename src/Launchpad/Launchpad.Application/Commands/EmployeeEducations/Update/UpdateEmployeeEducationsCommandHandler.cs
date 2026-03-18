using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeEducations.Update;

public class UpdateEmployeeEducationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateEmployeeEducationsCommandRequest, UpdateEmployeeEducationsCommandResponse>
{
    public async Task<UpdateEmployeeEducationsCommandResponse> Handle(UpdateEmployeeEducationsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateEmployeeEducationsCommandResponse();

        await applicationDbContext.EmployeeEducations
            .Where(x => x.Id == request.EducationId)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Organization, request.Organization)
                    .SetProperty(p => p.Faculty, request.Faculty)
                    .SetProperty(p => p.Specialization, request.Specialization)
                    .SetProperty(p => p.CompletionYear, request.CompletionYear)
                    .SetProperty(p => p.EducationLevelId, request.EducationLevelId)
                , cancellationToken);

        return response;
    }
}