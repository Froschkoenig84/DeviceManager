namespace Devices.Api.ResponseWrapper;

public class Meta
{
    public int? Count { get; set; }
    public string? Message { get; set; }
    public IEnumerable<Error>? Errors { get; set; }
}