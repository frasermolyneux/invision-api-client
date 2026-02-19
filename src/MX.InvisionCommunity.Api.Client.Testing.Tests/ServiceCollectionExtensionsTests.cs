using Microsoft.Extensions.DependencyInjection;

using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddFakeInvisionApiClient_RegistersAllServices()
    {
        var services = new ServiceCollection();

        services.AddFakeInvisionApiClient();

        var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetRequiredService<IInvisionApiClient>());
        Assert.NotNull(provider.GetRequiredService<ICoreApi>());
        Assert.NotNull(provider.GetRequiredService<IDownloadsApi>());
        Assert.NotNull(provider.GetRequiredService<IForumsApi>());
        Assert.NotNull(provider.GetRequiredService<FakeInvisionApiClient>());
    }

    [Fact]
    public void AddFakeInvisionApiClient_AllServicesShareSameFake()
    {
        var services = new ServiceCollection();

        services.AddFakeInvisionApiClient();

        var provider = services.BuildServiceProvider();

        var fake = provider.GetRequiredService<FakeInvisionApiClient>();
        var client = provider.GetRequiredService<IInvisionApiClient>();
        var core = provider.GetRequiredService<ICoreApi>();
        var downloads = provider.GetRequiredService<IDownloadsApi>();
        var forums = provider.GetRequiredService<IForumsApi>();

        Assert.Same(fake, client);
        Assert.Same(fake.CoreApi, core);
        Assert.Same(fake.DownloadsApi, downloads);
        Assert.Same(fake.ForumsApi, forums);
    }

    [Fact]
    public async Task AddFakeInvisionApiClient_ConfigureCallbackIsApplied()
    {
        var services = new ServiceCollection();

        services.AddFakeInvisionApiClient(fake =>
        {
            fake.CoreApi.AddMemberResponse("1", InvisionDtoFactory.CreateMember(id: 1, name: "Configured"));
        });

        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<IInvisionApiClient>();

        var result = await client.Core.GetMember("1");

        Assert.True(result.IsSuccess);
        Assert.Equal("Configured", result.Result?.Data?.Name);
    }

    [Fact]
    public void AddFakeInvisionApiClient_ReplacesExistingRegistrations()
    {
        var services = new ServiceCollection();

        // Register something first
        services.AddSingleton<ICoreApi>(new FakeCoreApi());

        // AddFake should replace it
        services.AddFakeInvisionApiClient();

        var provider = services.BuildServiceProvider();
        var allCore = provider.GetServices<ICoreApi>().ToList();

        Assert.Single(allCore);
    }
}
