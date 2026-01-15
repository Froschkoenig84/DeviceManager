namespace Devices.Domain.BaseModels;

public abstract class DetailedDeviceBase : OverviewDeviceBase
{
    public string Id { get; init; } = null!;
    public int TempMin { get; init; }
    public int TempMax { get; init; }
    public string InstallationPosition { get; init; } = null!;
    public bool? InsertInto19InchCabinet { get; init; }
    public bool? TerminalElement { get; init; }
    public bool? AdvancedEnvironmentalConditions { get; init; }
}