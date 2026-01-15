using Devices.Domain.Entities;
using Devices.Domain.Repositories;
using Devices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Devices.Infrastructure.Repositories;

public class DeviceRepository(DeviceDbContext context) : IDeviceRepository
{
    public async Task<IEnumerable<OverviewDevice>> GetOverviewAsync()
        => await context.FullDevices
            .Select(d => new OverviewDevice(
                d.Guid,
                d.Name,
                d.DeviceTypeId,
                d.Failsafe))
            .ToListAsync();

    public async Task<DetailedDevice?> GetDetailedDeviceByGuidAsync(Guid guid)
        => await context.FullDevices
            .Select(d => new DetailedDevice(
                d.Guid,
                d.Id,
                d.Name,
                d.DeviceTypeId,
                d.Failsafe,
                d.TempMin,
                d.TempMax,
                d.InstallationPosition,
                d.InsertInto19InchCabinet,
                d.TerminalElement,
                d.AdvancedEnvironmentalConditions
            ))
            .FirstOrDefaultAsync(d => d.Guid == guid);

    public async Task<FullDevice?> GetFullDeviceByGuidAsync(Guid guid)
        => await context.FullDevices.FirstOrDefaultAsync(d => d.Guid == guid);

    public async Task AddRangeAsync(IEnumerable<FullDevice> devices)
    {
        await context.FullDevices.AddRangeAsync(devices);
        await context.SaveChangesAsync();
    }

    public async Task DeleteByGuidAsync(Guid guid)
    {
        var device = await context.FullDevices.FirstOrDefaultAsync(d => d.Guid == guid);
        if (device == null) return;

        context.FullDevices.Remove(device);
        await context.SaveChangesAsync();
    }
}