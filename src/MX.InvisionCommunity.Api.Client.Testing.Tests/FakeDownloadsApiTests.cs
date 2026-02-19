using System.Net;

using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class FakeDownloadsApiTests
{
    [Fact]
    public async Task GetDownloadFile_ReturnsConfiguredResponse()
    {
        var fake = new FakeDownloadsApi();
        var dto = InvisionDtoFactory.CreateDownloadFile(id: 10, title: "My Download");
        fake.AddResponse(10, dto);

        var result = await fake.GetDownloadFile(10);

        Assert.True(result.IsSuccess);
        Assert.Equal(10, result.Result?.Data?.Id);
        Assert.Equal("My Download", result.Result?.Data?.Title);
    }

    [Fact]
    public async Task GetDownloadFile_ReturnsGenericSuccessWhenNotConfigured()
    {
        var fake = new FakeDownloadsApi();

        var result = await fake.GetDownloadFile(99);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Result?.Data);
        Assert.Equal(99, result.Result.Data.Id);
    }

    [Fact]
    public async Task GetDownloadFile_ReturnsErrorWhenDefaultBehaviorIsError()
    {
        var fake = new FakeDownloadsApi();
        fake.SetDefaultBehavior(DefaultFakeBehavior.ReturnError);

        var result = await fake.GetDownloadFile(99);

        Assert.True(result.IsNotFound);
    }

    [Fact]
    public async Task GetDownloadFile_ReturnsConfiguredErrorResponse()
    {
        var fake = new FakeDownloadsApi();
        fake.AddErrorResponse(10, HttpStatusCode.InternalServerError, "SERVER_ERROR", "Something went wrong");

        var result = await fake.GetDownloadFile(10);

        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }

    [Fact]
    public async Task GetDownloadFile_TracksLookedUpFiles()
    {
        var fake = new FakeDownloadsApi();

        await fake.GetDownloadFile(1);
        await fake.GetDownloadFile(2);

        Assert.Equal(2, fake.LookedUpFiles.Count);
        Assert.Contains(1, fake.LookedUpFiles);
        Assert.Contains(2, fake.LookedUpFiles);
    }

    [Fact]
    public async Task Reset_ClearsAllState()
    {
        var fake = new FakeDownloadsApi();
        fake.AddResponse(1, InvisionDtoFactory.CreateDownloadFile());
        await fake.GetDownloadFile(1);

        fake.Reset();

        Assert.Empty(fake.LookedUpFiles);
    }
}
