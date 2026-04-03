namespace Launchpad.Candidates.Domain.Errors;

public sealed record Error(string Code)
{
    public static readonly Error None = new Error(string.Empty);
}