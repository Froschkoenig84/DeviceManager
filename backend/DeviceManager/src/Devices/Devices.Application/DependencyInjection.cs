using Devices.Application.Interfaces;
using Devices.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Devices.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeviceService, DeviceService>();
        return services;
    }
}