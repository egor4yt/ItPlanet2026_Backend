using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.WorkFormats.GetAll;

public class GetAllWorkFormatsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetAllWorkFormatsQueryRequest, GetAllWorkFormatsQueryResponse>
{
    public async Task<GetAllWorkFormatsQueryResponse> Handle(GetAllWorkFormatsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllWorkFormatsQueryResponse();

        response.Items = await applicationDbContext.VacancyTypes
            .Select(x => new GetAllWorkFormatsQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}