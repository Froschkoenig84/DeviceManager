using Devices.Domain.Entities;

namespace Devices.Application.Interfaces;

public interface IDeviceService
{
    Task<IEnumerable<OverviewDevice>> GetOverviewDevicesAsync();
    Task<DetailedDevice> GetDetailedDeviceByGuidAsync(Guid guid);
    Task<FullDevice> GetFullDeviceByGuidAsync(Guid guid);
    Task<Guid[]> CreateDevicesAsync(IEnumerable<FullDevice> devices);
    Task DeleteDeviceByGuidAsync(Guid guid);
}