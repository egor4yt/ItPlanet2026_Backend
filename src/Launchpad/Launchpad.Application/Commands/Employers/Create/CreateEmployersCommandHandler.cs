using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Employers.Create;

public class CreateEmployersCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<CreateEmployersCommandRequest, CreateEmployersCommandResponse>
{
    public async Task<CreateEmployersCommandResponse> Handle(CreateEmployersCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateEmployersCommandResponse();

        var employerExists = await applicationDbContext.Employers.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (employerExists) throw new ConflictException("EmployerAlreadyExists");

        var newEmployer = new Employer
        {
            Email = request.Email,
            Description = string.Empty,
            CompanyName = request.CompanyName,
            RegisteredOn = DateTime.UtcNow
        };

        await applicationDbContext.Employers.AddAsync(newEmployer, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        response.EmployerId = newEmployer.Id;
        response.BearerToken = SecurityHelper.GenerateJwtToken(request.JwtDescriptorDetails, new JwtDetails(newEmployer));

        return response;
    }
}