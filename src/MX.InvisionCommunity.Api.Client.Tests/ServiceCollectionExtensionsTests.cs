using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Tests;

public class ServiceCollectionExtensionsTests
{
    private static void ConfigureBaseline(InvisionApiClientOptionsBuilder builder)
    {
        builder
            .WithBaseUrl("https://example.invisioncommunity.local")
            .WithApiKeyAuthentication("test-api-key", "x-api-key", MX.Api.Client.Configuration.ApiKeyLocation.Header)
            .WithCachePartition("unit-tests");
    }

    [Fact]
    public void AddInvisionApiClient_WithCombinedSubApiCacheExpressions_ResolvesAllSubApis()
    {
        var services = new ServiceCollection();

        // This scenario used to crash host startup: passing a WithCaching(...) delegate that mentions
        // multiple typed sub-APIs previously flowed the same builder-scoped delegate into every
        // AddTypedApiClient<>() call, so the second registration threw ArgumentException on the sibling's
        // expression. With SharedCacheConfiguration this must now succeed.
        services.AddInvisionApiClient(o =>
        {
            ConfigureBaseline(o);
            o.WithCaching(cache =>
            {
                cache.UseLibraryDefaults();
                cache.InMemory<IForumsApi, Task<ApiResult<PostTopicResultDto>>>(
                    x => x.PostTopic(default, default, default!, default!, default!, default),
                    TimeSpan.FromSeconds(60));
                cache.NotCached<ICoreApi, Task<ApiResult<CoreHelloDto>>>(
                    x => x.GetCoreHello(default));
                cache.InMemory<IDownloadsApi, Task<ApiResult<DownloadFileDto>>>(
                    x => x.GetDownloadFile(default, default),
                    TimeSpan.FromSeconds(30));
            });
        });

        using var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetRequiredService<ICoreApi>());
        Assert.NotNull(provider.GetRequiredService<IDownloadsApi>());
        Assert.NotNull(provider.GetRequiredService<IForumsApi>());
        Assert.NotNull(provider.GetRequiredService<IInvisionApiClient>());
    }

    [Fact]
    public void AddInvisionApiClient_WithoutCaching_ResolvesAllSubApis()
    {
        var services = new ServiceCollection();

        services.AddInvisionApiClient(ConfigureBaseline);

        using var provider = services.BuildServiceProvider();

        Assert.NotNull(provider.GetRequiredService<ICoreApi>());
        Assert.NotNull(provider.GetRequiredService<IDownloadsApi>());
        Assert.NotNull(provider.GetRequiredService<IForumsApi>());
        Assert.NotNull(provider.GetRequiredService<IInvisionApiClient>());
    }

    [Fact]
    public void AddInvisionApiClient_WithBogusInterfaceExpression_ThrowsInvalidOperationException()
    {
        var services = new ServiceCollection();

        // IBogusApi is not registered as one of the typed sub-APIs on this client, so
        // ValidateAllOperationsMatched() must surface the typo at composition time.
        var ex = Assert.Throws<InvalidOperationException>(() =>
            services.AddInvisionApiClient(o =>
            {
                ConfigureBaseline(o);
                o.WithCaching(cache =>
                {
                    cache.InMemory<IBogusApi, Task<ApiResult<CoreHelloDto>>>(
                        x => x.SomeMethod(default),
                        TimeSpan.FromSeconds(30));
                });
            }));

        Assert.Contains("IBogusApi", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    public interface IBogusApi
    {
        Task<ApiResult<CoreHelloDto>> SomeMethod(CancellationToken cancellationToken = default);
    }
}
