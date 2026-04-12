using System.Diagnostics.Metrics;

namespace Launchpad.Warehouse.Application.Metrics;

public static class WarehouseMeter
{
    private static readonly Meter Meter = new Meter("Launchpad.Warehouse", "1.0.0");
    public static readonly Counter<int> MediatrHandlerExecuted = Meter.CreateCounter<int>("mediatr_handler_executed");
}