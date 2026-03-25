using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;

namespace Launchpad.Application.Commands.Vacancies.Create;

public class CreateVacanciesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateVacanciesCommandRequest, CreateVacanciesCommandResponse>
{
    public async Task<CreateVacanciesCommandResponse> Handle(CreateVacanciesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateVacanciesCommandResponse();

        var newVacancy = new Vacancy
        {
            Title = request.Title,
            Description = request.Description,
            Location = GeometryHelper.CreatePoint(request.Longitude, request.Latitude),
            CreatedAt = DateTime.UtcNow,
            EmployerId = request.EmployerId
        };
        await applicationDbContext.Vacancies.AddAsync(newVacancy, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        
        response.VacancyId = newVacancy.Id;
        
        return response;
    }
}