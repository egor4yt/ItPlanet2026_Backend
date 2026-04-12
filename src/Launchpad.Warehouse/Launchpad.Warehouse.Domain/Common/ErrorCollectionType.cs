namespace Launchpad.Warehouse.Domain.Common;

public enum ErrorCollectionType
{
    InvalidOperation = 1,
    ResourceNotFound = 2,
    Conflict = 3,
    Unathorized = 4,
    Forbidden = 5,
    ValidationError = 6
}