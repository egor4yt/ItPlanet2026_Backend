using Launchpad.Warehouse.Domain.Common;

namespace Launchpad.Warehouse.Domain.Errors;

public static class DomainErrors
{
    public static class Skill
    {
        public static readonly Error AlreadyProcessed = new Error("ALREADY_PROCESSED", "Already processed");
    }
}