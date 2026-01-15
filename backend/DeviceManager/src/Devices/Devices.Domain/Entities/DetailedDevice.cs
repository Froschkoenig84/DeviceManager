namespace Devices.Domain.Entities;

public class DetailedDevice : BaseModels.DetailedDeviceBase
{
    public Guid Guid { get; }

    public DetailedDevice(Guid guid, string id, string name, string deviceTypeId, bool failsafe, int tempMin, int tempMax, string installationPosition, bool? insertInto19InchCabinet, bool? terminalElement, bool? advancedEnvironmentalConditions)
    {
        Guid = guid;
        Id = string.IsNullOrWhiteSpace(id)
            ? throw new ArgumentException("Id is required", nameof(id))
            : id;
        Name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Name is required", nameof(name))
            : name;
        DeviceTypeId = string.IsNullOrWhiteSpace(deviceTypeId)
            ? throw new ArgumentException("DeviceTypeId is required", nameof(deviceTypeId))
            : deviceTypeId;
        Failsafe = failsafe;
        if (tempMin > tempMax)
            throw new ArgumentException("TempMin must be less than or equal to TempMax");
        TempMin = tempMin;
        TempMax = tempMax;
        InstallationPosition = installationPosition;
        InsertInto19InchCabinet = insertInto19InchCabinet;
        TerminalElement = terminalElement;
        AdvancedEnvironmentalConditions = advancedEnvironmentalConditions;
    }
    public DetailedDevice(FullDevice device)
        : this(device.Guid, device.Id, device.Name, device.DeviceTypeId, device.Failsafe, device.TempMin, device.TempMax, device.InstallationPosition, device.InsertInto19InchCabinet, device.TerminalElement, device.AdvancedEnvironmentalConditions)
    {}
}