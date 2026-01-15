using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using Devices.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Devices.Application.Services;

public class DeviceService(IDeviceRepository repository, ILogger<DeviceService> logger)
    : IDeviceService
{
    private readonly ILogger<DeviceService> _logger = logger;

    public async Task<IEnumerable<OverviewDevice>> GetOverviewDevicesAsync()
        => await repository.GetOverviewAsync();

    public async Task<DetailedDevice> GetDetailedDeviceByGuidAsync(Guid guid)
        => await repository.GetDetailedDeviceByGuidAsync(guid)
           ?? throw new KeyNotFoundException($"Device with guid {guid} not found.");
    
    public async Task<FullDevice> GetFullDeviceByGuidAsync(Guid guid)
        => await repository.GetFullDeviceByGuidAsync(guid)
           ?? throw new KeyNotFoundException($"Device with guid {guid} not found.");

    public async Task<Guid[]> CreateDevicesAsync(IEnumerable<FullDevice> devices)
    {
        var list = devices as FullDevice[] ?? devices.ToArray();
        await repository.AddRangeAsync(list);
        return list.Select(d => d.Guid).ToArray();
    }

    public async Task DeleteDeviceByGuidAsync(Guid guid)
        => await repository.DeleteByGuidAsync(guid);
}