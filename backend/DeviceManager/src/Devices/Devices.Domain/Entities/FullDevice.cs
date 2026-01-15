namespace Devices.Domain.Entities;

public class FullDevice : BaseModels.FullDeviceBase
{
    public Guid Guid { get; private set; }
    
    protected FullDevice() { } //ef serializer
    public FullDevice(
        string id,
        string name,
        string deviceTypeId,
        bool failsafe,
        int tempMin,
        int tempMax,
        string installationPosition,
        bool? insertInto19InchCabinet,
        bool? terminalElement,
        bool? advancedEnvironmentalConditions,
        bool? motionEnable,
        bool? siplusCatalog,
        bool? simaticCatalog,
        int rotationAxisNumber,
        int positionAxisNumber)
    {
        Guid = Guid.NewGuid();
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
        MotionEnable = motionEnable;
        SiplusCatalog = siplusCatalog;
        SimaticCatalog = simaticCatalog;
        RotationAxisNumber = rotationAxisNumber;
        PositionAxisNumber = positionAxisNumber;
    }
}
