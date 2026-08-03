using System;

using MX.Api.Client.Configuration;

namespace MX.InvisionCommunity.Api.Client
{
    public class InvisionApiClientOptionsBuilder : ApiClientOptionsBuilder<InvisionApiClientOptions, InvisionApiClientOptionsBuilder>
    {
        public InvisionApiClientOptionsBuilder() : base() { }

        public InvisionApiClientOptionsBuilder WithApiPathPrefix(string apiPathPrefix)
        {
            Options.ApiPathPrefix = apiPathPrefix;
            return this;
        }

        /// <summary>
        /// Captures the consumer's <see cref="ApiClientOptionsBuilder{TOptions, TBuilder}.WithCaching(Action{CacheBuilder})"/>
        /// delegate so <c>ServiceCollectionExtensions.AddInvisionApiClient</c> can promote it to a
        /// <c>SharedCacheConfiguration</c> shared across every typed sub-API registration.
        /// </summary>
        internal Action<CacheBuilder>? CapturedCacheConfigure { get; private set; }

        /// <summary>
        /// Captures the caching configuration delegate without applying it directly. The captured delegate is
        /// promoted to a single <c>SharedCacheConfiguration</c> and applied to every typed sub-API registration
        /// by <c>ServiceCollectionExtensions.AddInvisionApiClient</c>, so a single expression can reference
        /// operations across <c>ICoreApi</c>, <c>IDownloadsApi</c>, and <c>IForumsApi</c> without triggering
        /// the single-client scope check.
        /// </summary>
        /// <param name="configure">The caching configuration delegate. Must not be <c>null</c>.</param>
        /// <returns>The builder instance for chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="configure"/> is <c>null</c>.</exception>
        public new InvisionApiClientOptionsBuilder WithCaching(Action<CacheBuilder> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);
            CapturedCacheConfigure = configure;
            return this;
        }
    }
}
