using System;

using Microsoft.Extensions.DependencyInjection;

using MX.Api.Client.Configuration;
using MX.Api.Client.Extensions;
using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Client.Api;
using MX.InvisionCommunity.Api.Client.Caching;

namespace MX.InvisionCommunity.Api.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInvisionApiClient(this IServiceCollection serviceCollection,
            Action<InvisionApiClientOptionsBuilder> configureOptions)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);
            ArgumentNullException.ThrowIfNull(configureOptions);

            // Probe the consumer's configuration once to capture any WithCaching(...) delegate. This lets us build a
            // single SharedCacheConfiguration and reuse it across every typed sub-API registration below without
            // triggering MX.Api.Client's single-client scope check for cache expressions that target sibling
            // interfaces (see MX.Api.Client 2.3.77 SharedCacheConfiguration).
            var probe = new InvisionApiClientOptionsBuilder();
            configureOptions(probe);
            var capturedCache = probe.CapturedCacheConfigure;
            var sharedCache = capturedCache is null ? null : new SharedCacheConfiguration(capturedCache);

            void PerClient(InvisionApiClientOptionsBuilder builder)
            {
                configureOptions(builder);
                if (sharedCache is not null)
                {
                    builder.WithSharedCaching(sharedCache);
                }
            }

            // Register conservative library-owned default cache policies before the typed clients so that consumers
            // opting in via cache.UseLibraryDefaults() pick them up. Only read-only GET methods with no matching
            // mutation surface on this client are included (see InvisionApiCacheDefaults for rationale).
            serviceCollection.AddInvisionApiDefaultCachePolicies();

            serviceCollection.AddTypedApiClient<ICoreApi, CoreApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(PerClient);
            serviceCollection.AddTypedApiClient<IDownloadsApi, DownloadsApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(PerClient);
            serviceCollection.AddTypedApiClient<IForumsApi, ForumsApi, InvisionApiClientOptions, InvisionApiClientOptionsBuilder>(PerClient);

            // Fail fast on typos: any captured operation that didn't match one of the typed clients above surfaces
            // as InvalidOperationException at composition time rather than as a silent no-op at runtime.
            sharedCache?.ValidateAllOperationsMatched();

            serviceCollection.AddScoped<IInvisionApiClient, InvisionApiClient>();

            return serviceCollection;
        }
    }
}
