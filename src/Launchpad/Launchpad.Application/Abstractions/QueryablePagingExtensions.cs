namespace Launchpad.Application.Abstractions;

public static class QueryablePagingExtensions
{
    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, IPagingRequest request)
    {
        return queryable
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
    }
}