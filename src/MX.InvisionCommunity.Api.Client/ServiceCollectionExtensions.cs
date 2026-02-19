using Microsoft.Extensions.DependencyInjection;

using MX.Api.Client.Extensions;
using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Client.Api;

namespace MX.InvisionCommunity.Api.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInvisionApiClient(this IServiceCollection serviceCollection,
            Action<InvisionApiClientOptionsBuilder> configureOptions)
        {
            serviceCollection.AddTypedApiClient<ICoreApi, CoreApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(configureOptions);
            serviceCollection.AddTypedApiClient<IDownloadsApi, DownloadsApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(configureOptions);
            serviceCollection.AddTypedApiClient<IForumsApi, ForumsApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(configureOptions);

            serviceCollection.AddScoped<IInvisionApiClient, InvisionApiClient>();

            return serviceCollection;
        }
    }
}