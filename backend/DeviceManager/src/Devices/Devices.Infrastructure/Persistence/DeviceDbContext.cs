using Devices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devices.Infrastructure.Persistence;

public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options)
{
    public DbSet<FullDevice> FullDevices => Set<FullDevice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FullDevice>(entity =>
        {
            entity.HasKey(d => d.Guid);

            entity.Property(d => d.Guid)
                .ValueGeneratedNever();

            entity.Property(d => d.Id)
                .IsRequired();

            entity.Property(d => d.Name)
                .IsRequired();

            entity.Property(d => d.DeviceTypeId)
                .IsRequired();

            entity.Property(d => d.Failsafe)
                .IsRequired();
        });
    }
}