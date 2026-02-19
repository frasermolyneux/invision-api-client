using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;

namespace MX.InvisionCommunity.Api.Client.Testing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeInvisionApiClient(
        this IServiceCollection services,
        Action<FakeInvisionApiClient>? configure = null)
    {
        var fake = new FakeInvisionApiClient();
        configure?.Invoke(fake);

        services.RemoveAll<ICoreApi>();
        services.RemoveAll<IDownloadsApi>();
        services.RemoveAll<IForumsApi>();
        services.RemoveAll<IInvisionApiClient>();

        services.AddSingleton<FakeInvisionApiClient>(fake);
        services.AddSingleton<ICoreApi>(fake.CoreApi);
        services.AddSingleton<IDownloadsApi>(fake.DownloadsApi);
        services.AddSingleton<IForumsApi>(fake.ForumsApi);
        services.AddSingleton<IInvisionApiClient>(fake);

        return services;
    }
}
