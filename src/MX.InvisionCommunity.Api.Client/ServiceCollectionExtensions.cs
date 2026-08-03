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

            // Lazily capture the consumer's WithCaching(...) delegate the first time PerClient runs so that we
            // invoke configureOptions exactly once per typed sub-API registration (matching the pre-change
            // behaviour: no extra invocation for side effects, expensive setup, or one-time reads). The captured
            // delegate is then wrapped in a single SharedCacheConfiguration and reused across every subsequent
            // registration, which is what lets MX.Api.Client 2.3.77 apply the sibling-interface expressions without
            // triggering the single-client scope check.
            SharedCacheConfiguration? sharedCache = null;

            void PerClient(InvisionApiClientOptionsBuilder builder)
            {
                configureOptions(builder);

                if (sharedCache is null && builder.CapturedCacheConfigure is not null)
                {
                    sharedCache = new SharedCacheConfiguration(builder.CapturedCacheConfigure);
                }

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
