using System.Net;

using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class FakeInvisionApiClientTests
{
    [Fact]
    public void Core_DelegatesToFakeCoreApi()
    {
        var fake = new FakeInvisionApiClient();

        Assert.Same(fake.CoreApi, fake.Core);
    }

    [Fact]
    public void Downloads_DelegatesToFakeDownloadsApi()
    {
        var fake = new FakeInvisionApiClient();

        Assert.Same(fake.DownloadsApi, fake.Downloads);
    }

    [Fact]
    public void Forums_DelegatesToFakeForumsApi()
    {
        var fake = new FakeInvisionApiClient();

        Assert.Same(fake.ForumsApi, fake.Forums);
    }

    [Fact]
    public async Task Reset_ClearsAllFakeState()
    {
        var fake = new FakeInvisionApiClient();

        // Build up some state
        fake.CoreApi.AddMemberResponse("1", InvisionDtoFactory.CreateMember(id: 1));
        await fake.Core.GetMember("1");

        fake.DownloadsApi.AddResponse(1, InvisionDtoFactory.CreateDownloadFile(id: 1));
        await fake.Downloads.GetDownloadFile(1);

        await fake.Forums.PostTopic(1, 1, "Test", "Body", "prefix");

        // Reset
        fake.Reset();

        Assert.Empty(fake.CoreApi.LookedUpMembers);
        Assert.Empty(fake.DownloadsApi.LookedUpFiles);
        Assert.Empty(fake.ForumsApi.PostedTopics);
    }
}
