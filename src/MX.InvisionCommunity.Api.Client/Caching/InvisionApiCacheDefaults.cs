using System;

using Microsoft.Extensions.DependencyInjection;

using MX.Api.Abstractions;
using MX.Api.Client.Configuration;
using MX.Api.Client.Extensions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Caching
{
    /// <summary>
    /// Library-owned default cache policies for the Invision Community API client.
    /// </summary>
    /// <remarks>
    /// Only operations whose declaring interface represents an idempotent HTTP GET whose response is not mutated by any
    /// method exposed on this client are given a default policy. All create/update/delete-style methods
    /// (e.g. <see cref="IForumsApi.PostTopic"/>, <see cref="IForumsApi.UpdateTopic"/>) are intentionally excluded so
    /// that consumers do not receive stale writes served from cache.
    ///
    /// TTLs are deliberately short (30&#8211;60&#160;seconds) so that consumers who opt in still see near-real-time data
    /// while benefiting from de-duplication of tight polling loops. Consumers can override any policy via
    /// <c>WithCaching</c> on the options builder.
    /// </remarks>
    public static class InvisionApiCacheDefaults
    {
        internal static readonly TimeSpan ShortReadTtl = TimeSpan.FromSeconds(30);
        internal static readonly TimeSpan MediumReadTtl = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Registers conservative read-only default cache policies for the sub-APIs of this client.
        /// </summary>
        public static IServiceCollection AddInvisionApiDefaultCachePolicies(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            // ICoreApi:
            //  - GetCoreHello: idempotent GET returning API metadata; no mutation methods exist for it. Safe.
            //  - GetMember: idempotent GET; this client exposes no member mutation methods, so cached reads only go
            //    stale relative to changes made outside the process. Short TTL keeps that window small.
            services.AddDefaultCachePolicies<ICoreApi>(cache =>
            {
                cache.InMemory<ICoreApi, Task<ApiResult<CoreHelloDto>>>(
                    x => x.GetCoreHello(default),
                    MediumReadTtl);
                cache.InMemory<ICoreApi, Task<ApiResult<MemberDto>>>(
                    x => x.GetMember(default!, default),
                    ShortReadTtl);
            });

            // IDownloadsApi:
            //  - GetDownloadFile: idempotent GET; this client exposes no downloads mutation methods. Safe with a
            //    short TTL to reflect out-of-band changes (e.g. new file versions) reasonably quickly.
            services.AddDefaultCachePolicies<IDownloadsApi>(cache =>
            {
                cache.InMemory<IDownloadsApi, Task<ApiResult<DownloadFileDto>>>(
                    x => x.GetDownloadFile(default, default),
                    ShortReadTtl);
            });

            // IForumsApi: intentionally no defaults. The only surfaced methods are PostTopic and UpdateTopic, both of
            // which are mutations and must not be cached.

            return services;
        }
    }
}
