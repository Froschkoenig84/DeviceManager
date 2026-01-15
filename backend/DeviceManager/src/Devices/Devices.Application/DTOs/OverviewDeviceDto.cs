using System;
using Devices.Domain.BaseModels;

namespace Devices.Application.DTOs;

public class OverviewDeviceDto : OverviewDeviceBase
{
    public Guid Guid { get; set; }
}