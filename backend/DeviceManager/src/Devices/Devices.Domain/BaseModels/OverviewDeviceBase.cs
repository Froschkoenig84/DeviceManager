namespace Devices.Domain.BaseModels;

public abstract class OverviewDeviceBase
{
    public string Name { get; init; } = null!;
    public string DeviceTypeId { get; init; } = null!;
    public bool Failsafe { get; init; }
}