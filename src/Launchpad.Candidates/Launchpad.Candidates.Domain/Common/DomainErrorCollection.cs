using System.Net;

namespace Launchpad.Candidates.Domain.Common;

public record DomainErrorCollection(IEnumerable<DomainError> Errors, HttpStatusCode StatusCode)
{
    public DomainErrorCollection(DomainError error, HttpStatusCode statusCode) : this([error], statusCode)
    {
    }
}