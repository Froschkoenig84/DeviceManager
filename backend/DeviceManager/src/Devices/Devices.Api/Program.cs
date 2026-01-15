using Devices.Api.ResponseWrapper;
using Devices.Application.Interfaces;
using Devices.Application.Services;
using Devices.Domain.Repositories;
using Devices.Infrastructure.Persistence;
using Devices.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllersWithValidationResponseWrapper();
//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ef+db (in-memory) 
builder.Services.AddDbContext<DeviceDbContext>(options =>
    options.UseInMemoryDatabase("DeviceDb"));
//interfaces (application.services) 
builder.Services.AddScoped<IDeviceService, DeviceService>();
//interfaces (domain » infrastructure.repository)
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();