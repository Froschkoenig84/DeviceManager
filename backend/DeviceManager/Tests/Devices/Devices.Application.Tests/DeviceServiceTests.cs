using Devices.Application.Services;
using Devices.Application.Tests.Helpers;
using Devices.Domain.Entities;
using Devices.Domain.Repositories;
using Moq;

namespace Devices.Application.Tests
{
    public class DeviceServiceTests
    {
        private readonly Mock<IDeviceRepository> _repositoryMock;
        private readonly DeviceService _service;

        public DeviceServiceTests()
        {
            _repositoryMock = new Mock<IDeviceRepository>();
            _service = new DeviceService(_repositoryMock.Object, new FakeLogger<DeviceService>());

        }

        [Fact]
        public async Task GetOverviewDevicesAsync_ReturnsAllDevices()
        {
            // Arrange
            var devices = new List<OverviewDevice>
            {
                new(Guid.NewGuid(), "Device1", "1", false),
                new(Guid.NewGuid(), "Device2", "2", true)
            };

            _repositoryMock.Setup(r => r.GetOverviewAsync())
                           .ReturnsAsync(devices);

            // Act
            var result = await _service.GetOverviewDevicesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDetailedDeviceByGuidAsync_Throws_WhenNotFound()
        {
            var guid = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetDetailedDeviceByGuidAsync(guid))
                           .ReturnsAsync((DetailedDevice?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _service.GetDetailedDeviceByGuidAsync(guid));
        }

        [Fact]
        public async Task CreateDevicesAsync_CallsRepository()
        {
            var devices = new List<FullDevice>
            {
                new( "abc123", "NameA", "TypeA", false, 0, 10, "InstPos", false, null, null, null, null, null, 0, 0),
                new( "def456", "NameB", "TypeB", true, 0, 0, "InstPos", true, true, false, true, false, null, 6, 9)
            };

            await _service.CreateDevicesAsync(devices);

            _repositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<FullDevice>>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDeviceByGuidAsync_CallsRepository()
        {
            var guid = Guid.NewGuid();

            await _service.DeleteDeviceByGuidAsync(guid);

            _repositoryMock.Verify(r => r.DeleteByGuidAsync(guid), Times.Once);
        }
    }
}
