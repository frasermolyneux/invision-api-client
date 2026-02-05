using MX.InvisionCommunity.Api.Client.Models;

namespace MX.InvisionCommunity.Api.Client.Interfaces
{
    public interface IDownloadsApi
    {
        Task<DownloadFile?> GetDownloadFile(int fileId);
    }
}