using System.Text.Json.Serialization;

namespace Launchpad.Application.Abstrcations;

[method: JsonConstructor]
public class PagedResult<T>(IReadOnlyCollection<T> items, int currentPage, int totalPages, int totalItems)
{
    public PagedResult(IReadOnlyCollection<T> items, int totalCount, IPaging pagingSettings) :
        this(items, pagingSettings.PageNumber, (int)Math.Ceiling(totalCount / (double)pagingSettings.PageSize), totalCount)
    {
    }

    public IReadOnlyCollection<T> Items { get; } = items;
    public int CurrentPage { get; init; } = currentPage;
    public int TotalPages { get; init; } = totalPages;
    public int TotalItems { get; init; } = totalItems;
}