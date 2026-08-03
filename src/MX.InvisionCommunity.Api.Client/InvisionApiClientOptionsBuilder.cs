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

        internal Action<CacheBuilder>? CapturedCacheConfigure { get; private set; }

        public new InvisionApiClientOptionsBuilder WithCaching(Action<CacheBuilder> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);
            CapturedCacheConfigure = configure;
            return this;
        }
    }
}
