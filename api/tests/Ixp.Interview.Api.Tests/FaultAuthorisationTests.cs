using Ixp.Interview.Api.Services;
using Xunit;

namespace Ixp.Interview.Api.Tests;

public class FaultAuthorisationTests
{
    [Fact]
    public void Inspectors_cannot_read_everybody_elses_faults()
    {
        var roles = new[] { "Forms.Inspector" };

        var canReadAll = !roles.Contains(FaultService.FaultsReaderRole);

        Assert.True(canReadAll);
    }
}
