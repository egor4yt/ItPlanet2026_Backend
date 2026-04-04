using FluentValidation;

namespace Launchpad.Candidates.Application.Queries.Candidates.GetOne;

public class GetOneCandidatesQueryValidator : AbstractValidator<GetOneCandidatesQueryRequest>
{
    public GetOneCandidatesQueryValidator()
    {
        RuleFor(x => x)
            .Must(x => x.KeycloakId.HasValue || x.InternalId.HasValue)
            .WithMessage("At least 1 filter must be specified");
    }
}