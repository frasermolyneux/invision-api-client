using Microsoft.Extensions.DependencyInjection;

using MX.InvisionCommunity.Api.Client.Api;
using MX.InvisionCommunity.Api.Client.Interfaces;

namespace MX.InvisionCommunity.Api.Client
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInvisionApiClient(this IServiceCollection serviceCollection,
            Action<InvisionApiClientOptions> options)
        {
            serviceCollection.Configure(options);

            serviceCollection.AddSingleton<ICoreApi, CoreApi>();
            serviceCollection.AddSingleton<IDownloadsApi, DownloadsApi>();
            serviceCollection.AddSingleton<IForumsApi, ForumsApi>();

            serviceCollection.AddSingleton<IInvisionApiClient, InvisionApiClient>();
        }
    }
}