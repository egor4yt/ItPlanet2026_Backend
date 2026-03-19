using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employers.UpdateDescription;

public class UpdateDescriptionEmployersCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<UpdateDescriptionEmployersCommandRequest>
{
    public async Task Handle(UpdateDescriptionEmployersCommandRequest request, CancellationToken cancellationToken)
    {
        await applicationDbContext.Employers
            .Where(x => x.Id == request.EmployerId)
            .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Description, string.IsNullOrWhiteSpace(request.Description) ? null : request.Description)
                , cancellationToken);
    }
}