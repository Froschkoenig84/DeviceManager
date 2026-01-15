using System.Collections.Generic;
using System.Linq;
using Devices.Application.DTOs;
using Devices.Domain.Entities;

namespace Devices.Application.Mappers;

public static class DeviceMapper
{
    public static OverviewDeviceDto ToOverviewDto(this OverviewDevice device)
        => new()
        {
            Guid = device.Guid,
            Name = device.Name,
            DeviceTypeId = device.DeviceTypeId,
            Failsafe = device.Failsafe
        };

    public static DetailedDeviceDto ToDeviceDto(this DetailedDevice detailedDevice)
        => new()
        {
            Guid = detailedDevice.Guid,
            Id = detailedDevice.Id,
            Name = detailedDevice.Name,
            DeviceTypeId = detailedDevice.DeviceTypeId,
            Failsafe = detailedDevice.Failsafe,
            TempMin = detailedDevice.TempMin,
            TempMax = detailedDevice.TempMax,
            InstallationPosition = detailedDevice.InstallationPosition,
            InsertInto19InchCabinet = detailedDevice.InsertInto19InchCabinet,
            TerminalElement = detailedDevice.TerminalElement,
            AdvancedEnvironmentalConditions = detailedDevice.AdvancedEnvironmentalConditions
        };
    
    public static FullDeviceDto ToFullDto(this FullDevice fullDevice)
        => new()
        {
            Guid = fullDevice.Guid,
            Id = fullDevice.Id,
            Name = fullDevice.Name,
            DeviceTypeId = fullDevice.DeviceTypeId,
            Failsafe = fullDevice.Failsafe,
            TempMin = fullDevice.TempMin,
            TempMax = fullDevice.TempMax,
            InstallationPosition = fullDevice.InstallationPosition,
            InsertInto19InchCabinet = fullDevice.InsertInto19InchCabinet,
            TerminalElement = fullDevice.TerminalElement,
            AdvancedEnvironmentalConditions = fullDevice.AdvancedEnvironmentalConditions,
            MotionEnable = fullDevice.MotionEnable,
            SiplusCatalog = fullDevice.SiplusCatalog,
            SimaticCatalog= fullDevice.SimaticCatalog,
            RotationAxisNumber = fullDevice.RotationAxisNumber,
            PositionAxisNumber = fullDevice.PositionAxisNumber
        };

    public static FullDevice ToEntity(this CreateDeviceDto dto)
        => new (
            dto.Id,
            dto.Name,
            dto.DeviceTypeId,
            dto.Failsafe,
            dto.TempMin,
            dto.TempMax,
            dto.InstallationPosition,
            dto.InsertInto19InchCabinet,
            dto.TerminalElement,
            dto.AdvancedEnvironmentalConditions,
            dto.MotionEnable,
            dto.SiplusCatalog,
            dto.SimaticCatalog,
            dto.RotationAxisNumber,
            dto.PositionAxisNumber
        );
    

    public static IEnumerable<OverviewDeviceDto> ToOverviewDtos(this IEnumerable<OverviewDevice> devices)
        => devices.Select(d => d.ToOverviewDto());

    public static IEnumerable<DetailedDeviceDto> ToDeviceDtos(this IEnumerable<DetailedDevice> devices)
        => devices.Select(d => d.ToDeviceDto());

    public static IEnumerable<FullDevice> ToEntities(this IEnumerable<CreateDeviceDto> dtos)
        => dtos.Select(d => d.ToEntity());
}
