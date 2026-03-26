using Launchpad.Application.Exceptions;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Queries.EmployerVerifications.Action;

public class GetOneEmployerVerificationsQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetOneEmployerVerificationsQueryRequest, GetOneEmployerVerificationsQueryResponse>
{
    public async Task<GetOneEmployerVerificationsQueryResponse> Handle(GetOneEmployerVerificationsQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetOneEmployerVerificationsQueryResponse();

        var query = applicationDbContext.EmployerVerifications
            .Include(x => x.Status)
            .Include(x => x.Type)
            .Include(x => x.Employer)
            .Where(x => x.Id == request.VerificationId);

        if (request.EmployerId.HasValue)
            query = query.Where(x => x.EmployerId == request.EmployerId);

        var verification = await query.FirstOrDefaultAsync(cancellationToken);
        if (verification == null) throw new NotFoundException("VerificationNotFound");

        response.CompanyName= verification.Employer!.CompanyName;
        response.RequestMessage = verification.RequestMessage;
        response.ChangedOn = verification.ChangedOn;
        response.ResponseMessage = verification.ResponseMessage;
        response.SocialNetworkLink = verification.SocialNetworkLink;
        response.TaxpayerIndividualNumber = verification.TaxpayerIndividualNumber;
        response.Type = new GetOneEmployerVerificationsQueryResponseType
        {
            Id = verification.EmployerVerificationTypeId,
            Title = verification.Type!.Title
        };
        response.Status = new GetOneEmployerVerificationsQueryResponseStatus
        {
            Id = verification.StatusId,
            Title = verification.Status!.Title
        };

        return response;
    }
}