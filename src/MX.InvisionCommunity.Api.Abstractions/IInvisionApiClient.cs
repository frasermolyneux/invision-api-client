using MX.InvisionCommunity.Api.Abstractions.Interfaces;

namespace MX.InvisionCommunity.Api.Abstractions
{
    public interface IInvisionApiClient
    {
        ICoreApi Core { get; }
        IDownloadsApi Downloads { get; }
        IForumsApi Forums { get; }
    }
}
