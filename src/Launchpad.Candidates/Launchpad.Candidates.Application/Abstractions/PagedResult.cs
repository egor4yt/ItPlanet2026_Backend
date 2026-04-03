using System.Text.Json.Serialization;

namespace Launchpad.Candidates.Application.Abstractions;

[method: JsonConstructor]
public class PagedResult<T>(IReadOnlyCollection<T> items, int currentPage, int totalPages, int totalItems)
{
    public PagedResult(IReadOnlyCollection<T> items, int totalCount, IPagingRequest pagingRequestSettings) :
        this(items, pagingRequestSettings.PageNumber, (int)Math.Ceiling(totalCount / (double)pagingRequestSettings.PageSize), totalCount)
    {
    }

    public IReadOnlyCollection<T> Items { get; } = items;
    public int CurrentPage { get; init; } = currentPage;
    public int TotalPages { get; init; } = totalPages;
    public int TotalItems { get; init; } = totalItems;
}