using Devices.Api.ResponseWrapper;
using Devices.Application.DTOs;
using Devices.Application.Interfaces;
using Devices.Application.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers;

[ApiController]
[Route("api/devices")]
public class DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger)
    : ControllerBase
{
    private readonly ILogger<DeviceController> _logger = logger;

    [HttpGet("overview")]
    public async Task<ActionResult<IEnumerable<OverviewDeviceDto>>> GetOverviewDevices()
    {
        var devices = await deviceService.GetOverviewDevicesAsync();
        return Ok(devices.ToOverviewDtos());
    }
    
    [HttpGet("{guid:guid}/detailed")] //detailed isn't all fields
    public async Task<ActionResult<DetailedDeviceDto>> GetDeviceByGuid(Guid guid)
    {
        try
        {
            var detailedDevice = await deviceService.GetDetailedDeviceByGuidAsync(guid);
            return Ok(detailedDevice.ToDeviceDto());
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpGet("{guid:guid}")] //full is root in RESTful
    public async Task<ActionResult<FullDeviceDto>> GetFullDeviceByGuid(Guid guid)
    {
        try
        {
            var fullDevice = await deviceService.GetFullDeviceByGuidAsync(guid);
            return Ok(fullDevice.ToFullDto());
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateDevices([FromBody] IEnumerable<CreateDeviceDto> createDtos)
    {
        var devices = createDtos.ToEntities();
        var guids = await deviceService.CreateDevicesAsync(devices);

        var response = ApiResponseHelper.CreateEntitiesResponse(
            guids,
            guid => Url.Action(nameof(GetFullDeviceByGuid), new { guid })!,
            "Devices successfully created"
        );

        return Created(string.Empty, response);
    }
    
    [HttpDelete("{guid:guid}")]
    public async Task<IActionResult> DeleteDeviceByGuid(Guid guid)
    {
        await deviceService.DeleteDeviceByGuidAsync(guid);
        return NoContent();
    }
}
