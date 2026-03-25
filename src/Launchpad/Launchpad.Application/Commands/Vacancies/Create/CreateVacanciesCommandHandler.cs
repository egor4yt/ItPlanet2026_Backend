using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Vacancies.Create;

public class CreateVacanciesCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateVacanciesCommandRequest, CreateVacanciesCommandResponse>
{
    public async Task<CreateVacanciesCommandResponse> Handle(CreateVacanciesCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateVacanciesCommandResponse();

        var companyVerificationStatusId = await applicationDbContext.EmployerVerifications
            .Where(x => x.EmployerId == request.EmployerId)
            .Select(x => x.StatusId)
            .FirstOrDefaultAsync(cancellationToken);

        if (companyVerificationStatusId != Domain.Metadata.EmployerVerificationStatusId.Approved)
            throw new ForbiddenException("NeedsVerification");

        var newVacancy = new Vacancy
        {
            Title = request.Title,
            Description = request.Description,
            Location = GeometryHelper.CreatePoint(request.Longitude, request.Latitude),
            CreatedAt = DateTime.UtcNow,
            EmployerId = request.EmployerId,
            TypeId = request.TypeId,
            StartDate =  request.StartDate,
            EndDate = request.EndDate,
            WorkFormats = request.WorkFormatIds.Select(x => new WorkFormat { Id = x, Title = string.Empty }).ToList(),
            Skills = request.Skills.Select(x => new Skill { Id = x.Id ?? 0, Title = x.Title }).ToList()
        };

        foreach (var workFormat in newVacancy.WorkFormats)
        {
            applicationDbContext.Entry(workFormat).State = EntityState.Unchanged;
        }

        foreach (var skill in newVacancy.Skills)
        {
            applicationDbContext.Entry(skill).State = skill.Id != 0 ? EntityState.Unchanged : EntityState.Added;
        }
        
        await applicationDbContext.Vacancies.AddAsync(newVacancy, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.VacancyId = newVacancy.Id;

        return response;
    }
}