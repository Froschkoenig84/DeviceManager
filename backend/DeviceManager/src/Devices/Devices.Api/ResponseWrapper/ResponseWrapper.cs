using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Devices.Api.ResponseWrapper;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddControllersWithValidationResponseWrapper(
        this IServiceCollection services)
    {
        return services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value?.Errors.Count > 0)
                        .SelectMany(ms => ms.Value!.Errors.Select(e => new Error
                        {
                            Field = ms.Key,
                            Message = e.ErrorMessage
                        }))
                        .ToList();

                    var response = new ApiResponse<object?>(
                        data: null,
                        meta: new Meta
                        {
                            Message = "Validation failed",
                            Errors = errors
                        });

                    return new BadRequestObjectResult(response);
                };
            });
    }
}