using Ixp.Interview.Api.Auth;
using Ixp.Interview.Api.Data;
using Ixp.Interview.Api.Models;
using Ixp.Interview.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

public class FaultLoadTests
{
    private class FakeUser : ICurrentUserAccessor
    {
        public CurrentUser User { get; } = new("test-user", "Test", ["Forms.Inspector"]);
    }

    [Fact]
    public void Initial_Load_Should_Return_First_Page()
    {
        var store = new FaultStore();
        for (var i = 0; i < 15; i++)
        {
            store.Add(new Fault { Id = Guid.NewGuid(), UserId = "test-user", IsSubmitted = true });
        }

        var service = new FaultService(
            store,
            new FaultCache(new MemoryCache(new MemoryCacheOptions())),
            new FakeUser(),
            new HttpContextAccessor());

        var result = service.GetFaults(null, null, null, 1, 10, "c1");

        Assert.Equal(10, result.Items.Count);
    }

    [Fact]
    public void Inspector_sees_only_own_faults()
   {
    var store = new FaultStore();
    store.Add(new Fault { Id = Guid.NewGuid(), UserId = "test-user", IsSubmitted = true });
    store.Add(new Fault { Id = Guid.NewGuid(), UserId = "someone-else", IsSubmitted = true });

    var service = new FaultService(store, new FaultCache(new MemoryCache(new MemoryCacheOptions())),
        new FakeUser(), new HttpContextAccessor());

    var result = service.GetFaults(null, null, null, 1, 10, "c1");

    Assert.All(result.Items, f => Assert.Equal("test-user", f.UserId));
   }
}