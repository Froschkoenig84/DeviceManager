using Devices.Application.Interfaces;
using Devices.Application.DTOs;
using Devices.Api.Controllers;
using Devices.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Devices.Api.Tests
{
    public class DeviceControllerTests
    {
        private readonly Mock<IDeviceService> _serviceMock;
        private readonly DeviceController _controller;

        public DeviceControllerTests()
        {
            _serviceMock = new Mock<IDeviceService>();
            var loggerMock = Mock.Of<ILogger<DeviceController>>();
            _controller = new DeviceController(_serviceMock.Object, loggerMock);
        }

        
        [Fact]
        public async Task GetOverviewDevices_ReturnsOk_WithData()
        {
            var overviewList = new List<OverviewDevice>
            {
                new OverviewDevice(Guid.NewGuid(), "TestDevice", "abc123", false)
            };

            _serviceMock.Setup(s => s.GetOverviewDevicesAsync())
                .ReturnsAsync(overviewList);

            var result = await _controller.GetOverviewDevices();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsAssignableFrom<IEnumerable<OverviewDeviceDto>>(okResult.Value);
            Assert.Single(data);
        }

        
        [Fact]
        public async Task GetDeviceByGuid_ReturnsNotFound_WhenDeviceMissing()
        {
            // Arrange
            var guid = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetDetailedDeviceByGuidAsync(guid))
                .ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.GetDeviceByGuid(guid);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }



        [Fact]
        public async Task DeleteDeviceByGuid_ReturnsNoContent()
        {
            // Arrange
            var guid = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteDeviceByGuidAsync(guid)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteDeviceByGuid(guid);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _serviceMock.Verify(s => s.DeleteDeviceByGuidAsync(guid), Times.Once);
        }
    }
}
