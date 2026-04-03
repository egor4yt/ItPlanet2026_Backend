namespace Launchpad.Candidates.Domain.Common;

public record ErrorCollection(IEnumerable<Error> Errors, ErrorCollectionType ErrorCollectionType)
{
    public ErrorCollection(Error error, ErrorCollectionType errorCollectionType) : this([error], errorCollectionType)
    {
    }
}