namespace Ixp.Interview.Api.Models;

public sealed class FaultListResponse
{
    public IReadOnlyList<Fault> Items { get; set; } = [];
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string CorrelationId { get; set; } = string.Empty;
}
