using MediatR;

namespace Launchpad.Application.Queries.EmployerVerifications.Action;

public class GetOneEmployerVerificationsQueryRequest : IRequest<GetOneEmployerVerificationsQueryResponse>
{
    public long VerificationId { get; set; }
    public long? EmployerId { get; set; }
}