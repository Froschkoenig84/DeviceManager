using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Devices.Api.ResponseWrapper;

internal static class ValidationErrorMapper
{
    public static List<Error> Map(ModelStateDictionary modelState)
    {
        Console.WriteLine("VALIDATION MAPPER CALLED");
        return modelState
            .Where(ms => ms.Value?.Errors.Count > 0)
            .SelectMany(ms => ms.Value!.Errors.Select(e => new Error
            {
                Field = ms.Key,
                Message = e.ErrorMessage
            }))
            .ToList();
    }
}