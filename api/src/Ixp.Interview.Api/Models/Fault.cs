namespace Ixp.Interview.Api.Models;

public sealed class Fault
{
    public Guid Id { get; set; }
    public Guid ResponseId { get; set; }
    public string TemplateId { get; set; } = "sq_station";
    public string TemplateVersion { get; set; } = "v1";
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Title { get; set; } = "*";
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset? SubmittedAtUtc { get; set; }
    public bool IsSubmitted { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string UserDisplayName { get; set; } = string.Empty;
}
