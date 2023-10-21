using XtremeIdiots.InvisionCommunity.Models;

namespace XtremeIdiots.InvisionCommunity.Interfaces
{
    public interface IDownloadsApi
    {
        Task<DownloadFile?> GetDownloadFile(int fileId);
    }
}