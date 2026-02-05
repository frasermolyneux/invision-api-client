using MX.InvisionCommunity.Api.Client.Interfaces;

namespace MX.InvisionCommunity.Api.Client
{
    public interface IInvisionApiClient
    {
        ICoreApi Core { get; }
        IDownloadsApi Downloads { get; }
        IForumsApi Forums { get; }
    }
}