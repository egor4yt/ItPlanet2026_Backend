using MediatR;

namespace Launchpad.Application.Queries.Vacancies.GetOne;

public class GetOneVacanciesQueryRequest : IRequest<GetOneVacanciesQueryResponse>
{
    public required long Id { get; init; }
}