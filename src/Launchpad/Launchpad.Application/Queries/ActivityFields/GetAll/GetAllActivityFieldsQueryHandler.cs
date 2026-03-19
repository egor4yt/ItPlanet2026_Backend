using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.ActivityFields.GetAll;

public class GetAllActivityFieldsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetAllActivityFieldsQueryRequest, GetAllActivityFieldsQueryResponse>
{
    public async Task<GetAllActivityFieldsQueryResponse> Handle(GetAllActivityFieldsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetAllActivityFieldsQueryResponse();

        response.Groups = await applicationDbContext.ActivityFieldGroups
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .Select(activityFieldGroup => new GetAllActivityFieldsQueryResponseGroup
            {
                GroupId = activityFieldGroup.Id,
                Title = activityFieldGroup.Title,
                Items = activityFieldGroup.ActivityFields
                    .OrderBy(x => x.Title)
                    .Select(activityField => new GetAllActivityFieldsQueryResponseGroupItem
                    {
                        Id = activityField.Id,
                        Title = activityField.Title
                    })
            })
            .ToListAsync(cancellationToken);

        return response;
    }
}