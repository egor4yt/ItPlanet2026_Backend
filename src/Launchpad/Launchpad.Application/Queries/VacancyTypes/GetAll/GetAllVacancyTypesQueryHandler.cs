using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.VacancyTypes.GetAll;

public class GetAllVacancyTypesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetAllVacancyTypesQueryRequest, GetAllVacancyTypesQueryResponse>
{
    public async Task<GetAllVacancyTypesQueryResponse> Handle(GetAllVacancyTypesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllVacancyTypesQueryResponse();

        response.Items = await applicationDbContext.VacancyTypes
            .Select(x => new GetAllVacancyTypesQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);
        
        return response;
    }
}