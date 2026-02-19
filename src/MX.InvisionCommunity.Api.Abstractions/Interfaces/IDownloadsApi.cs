using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Abstractions.Interfaces
{
    public interface IDownloadsApi
    {
        Task<ApiResult<DownloadFileDto>> GetDownloadFile(int fileId, CancellationToken cancellationToken = default);
    }
}
