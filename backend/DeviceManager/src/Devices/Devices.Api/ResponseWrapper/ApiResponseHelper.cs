namespace Devices.Api.ResponseWrapper;

public static class ApiResponseHelper
{
    public static ApiResponse<IEnumerable<object>> CreateEntitiesResponse(IEnumerable<Guid> guids,
        Func<Guid, string> urlBuilder,
        string? message = null)
    {
        var enumerable = guids as Guid[] ?? guids.ToArray();
        var data = enumerable.Select(guid => new
        {
            guid,
            url = urlBuilder(guid)
        });

        var meta = new Meta
        {
            Count = enumerable.Count(),
            Message = message
        };

        return new ApiResponse<IEnumerable<object>>(data, meta);
    }
}