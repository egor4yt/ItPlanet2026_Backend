using Launchpad.Application.Exceptions;
using Launchpad.Application.SharedModels;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Vacancies.GetOne;

public class GetOneVacanciesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneVacanciesQueryRequest, GetOneVacanciesQueryResponse>
{
    public async Task<GetOneVacanciesQueryResponse> Handle(GetOneVacanciesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await applicationDbContext.Vacancies
            .Where(x => x.Id == request.Id)
            .Select(x => new GetOneVacanciesQueryResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Type = x.Type!.Title,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                CreatedAt = x.CreatedAt,
                WorkFormat = string.Join(", ", x.WorkFormats.Select(w => w.Title)),
                Employer = new GetOneVacanciesQueryResponseEmployer
                {
                    Id = x.EmployerId,
                    CompanyName = x.Employer!.CompanyName,
                    IsVerified = x.Employer.Verification != null && x.Employer.Verification.StatusId == Domain.Metadata.EmployerVerificationStatusId.Approved
                },
                Location = new GetOneVacanciesQueryResponseLocation
                {
                    City = x.City,
                    FullAddress = x.FullAddress,
                    Coordinates = new GeolocationPoint
                    {
                        Longitude = x.Location.Coordinate.X,
                        Latitude = x.Location.Coordinate.Y
                    }
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException("VacancyNotFound");
    }
}