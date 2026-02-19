using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;

namespace MX.InvisionCommunity.Api.Client
{
    public class InvisionApiClient : IInvisionApiClient
    {
        public InvisionApiClient(
            ICoreApi coreApiClient,
            IDownloadsApi downloadsApiClient,
            IForumsApi forumsApiClient)
        {
            Core = coreApiClient;
            Downloads = downloadsApiClient;
            Forums = forumsApiClient;
        }

        public ICoreApi Core { get; }
        public IDownloadsApi Downloads { get; }
        public IForumsApi Forums { get; }
    }
}