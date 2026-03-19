using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.Employers.GetOne;

public class GetOneEmployersQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneEmployersQueryRequest, GetOneEmployersQueryResponse>
{
    public async Task<GetOneEmployersQueryResponse> Handle(GetOneEmployersQueryRequest request, CancellationToken cancellationToken)
    {
        var employer = await applicationDbContext.Employers
            .AsNoTracking()
            .Include(x => x.ActivityFields)
            .ThenInclude(x => x.ActivityFieldGroup)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (employer == null) throw new NotFoundException("EmployerNotFound");

        var response = new GetOneEmployersQueryResponse();
        response.CompanyName = employer.CompanyName;
        response.Description = employer.Description;
        response.Verified = false;

        var activityGroupTitles = employer.ActivityFields
            .Select(x => x.ActivityFieldGroup.Title)
            .Distinct()
            .Order();
        response.ActivityFields = string.Join(", ", activityGroupTitles);

        return response;
    }
}