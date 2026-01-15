using Devices.Domain.Entities;

namespace Devices.Domain.Repositories;

public interface IDeviceRepository
{
    Task<IEnumerable<OverviewDevice>> GetOverviewAsync();
    Task<DetailedDevice?> GetDetailedDeviceByGuidAsync(Guid guid);
    Task<FullDevice?> GetFullDeviceByGuidAsync(Guid guid);
    Task AddRangeAsync(IEnumerable<FullDevice> devices);
    Task DeleteByGuidAsync(Guid guid);
}