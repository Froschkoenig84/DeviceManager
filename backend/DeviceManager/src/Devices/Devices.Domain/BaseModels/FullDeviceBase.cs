namespace Devices.Domain.BaseModels;

public abstract class FullDeviceBase : DetailedDeviceBase
{
    public bool? MotionEnable { get; init; }
    public bool? SiplusCatalog { get; init; }
    public bool? SimaticCatalog { get; init; }
    public int RotationAxisNumber { get; init; }
    public int PositionAxisNumber { get; init; }
}
