using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.EducationLevels.GetAll;

public class GetAllEducationLevelsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetAllEducationLevelsQueryRequest, GetAllEducationLevelsQueryResponse>
{
    public async Task<GetAllEducationLevelsQueryResponse> Handle(GetAllEducationLevelsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllEducationLevelsQueryResponse();

        response.Items = await applicationDbContext.EducationLevels
            .AsNoTracking()
            .Select(x => new GetAllEducationLevelsQueryResponseItem
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}