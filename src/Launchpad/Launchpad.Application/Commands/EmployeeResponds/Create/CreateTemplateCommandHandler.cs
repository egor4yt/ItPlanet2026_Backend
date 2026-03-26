using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.EmployeeResponds.Create;

public class CreateEmployeeRespondsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployeeRespondsCommandRequest, CreateEmployeeRespondsCommandResponse>
{
    public async Task<CreateEmployeeRespondsCommandResponse> Handle(CreateEmployeeRespondsCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployeeRespondsCommandResponse();

        var existingRespondId = await applicationDbContext.EmployeeResponds
            .AsNoTracking()
            .Where(x => x.EmployeeId == request.EmployeeId && x.VacancyId == request.VacancyId)
            .Select(x => (long?)x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (!existingRespondId.HasValue)
        {
            var newResponse = new EmployeeRespond
            {
                CreatedAt = DateTime.UtcNow,
                CoverMessage = request.CoverMessage,
                CompanyAnswer = null,
                StatusId = Domain.Metadata.EmployeeRespondStatus.Created,
                EmployeeId = request.EmployeeId,
                VacancyId = request.VacancyId
            };

            await applicationDbContext.EmployeeResponds.AddAsync(newResponse, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            existingRespondId = newResponse.Id;
        }

        response.Id = existingRespondId.Value;

        return response;
    }
}