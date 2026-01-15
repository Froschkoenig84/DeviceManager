namespace Devices.Api.ResponseWrapper;

public class ApiResponse<T>
{
    public T Data { get; set; } = default!;
    public Meta? Meta { get; set; }

    public ApiResponse(T data, Meta? meta = null)
    {
        Data = data;
        Meta = meta;
    }
}