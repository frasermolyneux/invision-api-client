using XtremeIdiots.InvisionCommunity.Interfaces;

namespace XtremeIdiots.InvisionCommunity
{
    public interface IInvisionApiClient
    {
        ICoreApi Core { get; }
        IDownloadsApi Downloads { get; }
        IForumsApi Forums { get; }
    }
}