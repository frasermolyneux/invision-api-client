using System.Net;

using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class FakeCoreApiTests
{
    [Fact]
    public async Task GetCoreHello_ReturnsDefaultWhenNotConfigured()
    {
        var fake = new FakeCoreApi();

        var result = await fake.GetCoreHello();

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Result?.Data);
        Assert.Equal("Test Community", result.Result.Data.CommunityName);
    }

    [Fact]
    public async Task GetCoreHello_ReturnsConfiguredResponse()
    {
        var fake = new FakeCoreApi();
        var dto = InvisionDtoFactory.CreateCoreHello(communityName: "My Community");
        fake.WithCoreHello(dto);

        var result = await fake.GetCoreHello();

        Assert.True(result.IsSuccess);
        Assert.Equal("My Community", result.Result?.Data?.CommunityName);
    }

    [Fact]
    public async Task GetMember_ReturnsConfiguredResponse()
    {
        var fake = new FakeCoreApi();
        var member = InvisionDtoFactory.CreateMember(id: 42, name: "John");
        fake.AddMemberResponse("42", member);

        var result = await fake.GetMember("42");

        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Result?.Data?.Id);
        Assert.Equal("John", result.Result?.Data?.Name);
    }

    [Fact]
    public async Task GetMember_ReturnsGenericSuccessWhenNotConfigured()
    {
        var fake = new FakeCoreApi();

        var result = await fake.GetMember("99");

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Result?.Data);
        Assert.Contains("99", result.Result.Data.Name);
    }

    [Fact]
    public async Task GetMember_ReturnsErrorWhenDefaultBehaviorIsError()
    {
        var fake = new FakeCoreApi();
        fake.SetDefaultBehavior(DefaultFakeBehavior.ReturnError);

        var result = await fake.GetMember("99");

        Assert.True(result.IsNotFound);
    }

    [Fact]
    public async Task GetMember_ReturnsConfiguredErrorResponse()
    {
        var fake = new FakeCoreApi();
        fake.AddMemberErrorResponse("42", HttpStatusCode.Forbidden, "FORBIDDEN", "Access denied");

        var result = await fake.GetMember("42");

        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        Assert.NotNull(result.Result?.Errors);
    }

    [Fact]
    public async Task GetMember_TracksLookedUpMembers()
    {
        var fake = new FakeCoreApi();

        await fake.GetMember("1");
        await fake.GetMember("2");
        await fake.GetMember("1");

        Assert.Equal(3, fake.LookedUpMembers.Count);
        Assert.Contains("1", fake.LookedUpMembers);
        Assert.Contains("2", fake.LookedUpMembers);
    }

    [Fact]
    public async Task GetMember_IsCaseInsensitive()
    {
        var fake = new FakeCoreApi();
        fake.AddMemberResponse("ABC", InvisionDtoFactory.CreateMember(id: 1, name: "Found"));

        var result = await fake.GetMember("abc");

        Assert.True(result.IsSuccess);
        Assert.Equal("Found", result.Result?.Data?.Name);
    }

    [Fact]
    public async Task Reset_ClearsAllState()
    {
        var fake = new FakeCoreApi();
        fake.AddMemberResponse("1", InvisionDtoFactory.CreateMember());
        fake.WithCoreHello(InvisionDtoFactory.CreateCoreHello(communityName: "Custom"));
        await fake.GetMember("1");

        fake.Reset();

        Assert.Empty(fake.LookedUpMembers);
        // After reset, GetCoreHello returns default again
        var result = await fake.GetCoreHello();
        Assert.Equal("Test Community", result.Result?.Data?.CommunityName);
    }
}
