﻿using Microsoft.Extensions.DependencyInjection;

using XtremeIdiots.InvisionCommunity.Api;
using XtremeIdiots.InvisionCommunity.Interfaces;

namespace XtremeIdiots.InvisionCommunity
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