using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using MediatR;

namespace Launchpad.Application.Commands.EmployeeEducations.Create;

public class CreateEmployeeEducationsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployeeEducationsCommandRequest, CreateEmployeeEducationsCommandResponse>
{
    public async Task<CreateEmployeeEducationsCommandResponse> Handle(CreateEmployeeEducationsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployeeEducationsCommandResponse();

        var newEducation = new EmployeeEducation
        {
            Organization = request.Organization,
            Faculty = request.Faculty,
            Specialization = request.Specialization,
            CompletionYear = request.CompletionYear,
            EducationLevelId = request.EducationLevelId,
            EmployeeId = request.EmployeeId
        };

        await applicationDbContext.EmployeeEducations.AddAsync(newEducation, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.Id = newEducation.Id;
        
        return response;
    }
}