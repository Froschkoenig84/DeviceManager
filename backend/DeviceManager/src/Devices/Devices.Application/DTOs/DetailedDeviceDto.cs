namespace Devices.Application.DTOs;
using Devices.Domain.BaseModels;

public class DetailedDeviceDto : DetailedDeviceBase
{
    public Guid Guid { get; set; }
}