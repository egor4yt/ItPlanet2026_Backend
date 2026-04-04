using CSharpFunctionalExtensions;

namespace Launchpad.Candidates.Domain.Common;

public record ErrorCollection(IEnumerable<Error> Errors, ErrorCollectionType ErrorCollectionType) : ICombine
{
    public ErrorCollection(Error error, ErrorCollectionType errorCollectionType) : this([error], errorCollectionType)
    {
    }

    public ICombine Combine(ICombine value)
    {
        if (value is not ErrorCollection otherCollection)
            throw new ArgumentException($"Expected ErrorCollection, but got {value.GetType().Name}");

        if (otherCollection.ErrorCollectionType != ErrorCollectionType)
            throw new InvalidOperationException("Can't combine different error collection types");

        return this with { Errors = Errors.Concat(otherCollection.Errors).ToList() };
    }
}