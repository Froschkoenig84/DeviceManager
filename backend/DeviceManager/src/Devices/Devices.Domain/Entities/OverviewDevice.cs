namespace Devices.Domain.Entities;

public class OverviewDevice : BaseModels.OverviewDeviceBase
{
    public Guid Guid { get; }

    public OverviewDevice(Guid guid, string name, string deviceTypeId, bool failsafe)
    {
        Guid = guid;
        Name = name;
        DeviceTypeId = deviceTypeId;
        Failsafe = failsafe;
    }
    public OverviewDevice(FullDevice device)
        : this(device.Guid, device.Name, device.DeviceTypeId, device.Failsafe)
    {}
}