using Ixp.Interview.Api.Models;

namespace Ixp.Interview.Api.Data;

public sealed class FaultStore
{
    private readonly List<Fault> _faults;
    private readonly object _gate = new();

    public FaultStore()
    {
        var alex = "entra-guid-inspector-001";
        var sam = "entra-guid-reader-001";

        _faults =
        [
            new Fault
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ResponseId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Description = "Platform 3 lighting out on the Leeds-bound side",
                Category = "Lighting",
                Area = "Platform 3",
                Location = "LDS",
                Title = "Platform lighting",
                CreatedAtUtc = DateTimeOffset.Parse("2026-08-20T08:15:00Z"),
                SubmittedAtUtc = DateTimeOffset.Parse("2026-08-20T08:16:00Z"),
                IsSubmitted = true,
                UserId = alex,
                UserDisplayName = "Alex Patel",
            },
            new Fault
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ResponseId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Description = "Waiting-room bench slat split — do not use",
                Category = "Station information & seating",
                Area = "Concourse",
                Location = "MAN",
                Title = "Broken seating",
                CreatedAtUtc = DateTimeOffset.Parse("2026-08-21T11:00:00Z"),
                SubmittedAtUtc = DateTimeOffset.Parse("2026-08-21T11:02:00Z"),
                IsSubmitted = true,
                UserId = sam,
                UserDisplayName = "Sam Okonkwo",
            },
            new Fault
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ResponseId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Description = "Lift car interior light flickering — still in progress",
                Category = "Lifts & Escalators",
                Area = "Lift A",
                Location = "LDS",
                Title = "Lift lighting (draft)",
                CreatedAtUtc = DateTimeOffset.Parse("2026-08-22T09:40:00Z"),
                SubmittedAtUtc = null,
                IsSubmitted = false,
                UserId = alex,
                UserDisplayName = "Alex Patel",
            },
        ];
    }

    public IReadOnlyList<Fault> Snapshot()
    {
        lock (_gate)
        {
            return _faults.ToList();
        }
    }

    public Fault Add(Fault fault)
    {
        lock (_gate)
        {
            _faults.Insert(0, fault);
            return fault;
        }
    }

    public Fault? Get(Guid id)
    {
        lock (_gate)
        {
            return _faults.FirstOrDefault(f => f.Id == id);
        }
    }
}
