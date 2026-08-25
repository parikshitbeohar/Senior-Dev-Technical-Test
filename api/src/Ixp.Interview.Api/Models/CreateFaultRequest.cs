namespace Ixp.Interview.Api.Models;

public sealed class CreateFaultRequest
{
    public string Category { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Title { get; set; }
}
