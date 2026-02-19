using System.Net;

using Microsoft.Extensions.Logging;

using MX.Api.Abstractions;
using MX.Api.Client;
using MX.Api.Client.Auth;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

using Newtonsoft.Json;

using RestSharp;

namespace MX.InvisionCommunity.Api.Client.Api
{
    public class DownloadsApi : BaseApi<InvisionApiClientOptions>, IDownloadsApi
    {
        public DownloadsApi(
            ILogger<BaseApi<InvisionApiClientOptions>> logger,
            IApiTokenProvider? apiTokenProvider,
            IRestClientService restClientService,
            InvisionApiClientOptions options)
            : base(logger, apiTokenProvider, restClientService, options)
        {
        }

        public async Task<ApiResult<DownloadFileDto>> GetDownloadFile(int fileId, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = await CreateRequestAsync($"api/downloads/files/{fileId}", Method.Get, cancellationToken);
                var response = await ExecuteAsync(request, cancellationToken);

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    return new ApiResult<DownloadFileDto>(response.StatusCode,
                        new ApiResponse<DownloadFileDto>(new ApiError("NO_CONTENT", "Response has no content")));
                }

                var data = JsonConvert.DeserializeObject<DownloadFileDto>(response.Content);
                return new ApiResult<DownloadFileDto>(response.StatusCode, new ApiResponse<DownloadFileDto>(data));
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new ApiResult<DownloadFileDto>(HttpStatusCode.InternalServerError,
                    new ApiResponse<DownloadFileDto>(new ApiError("CLIENT_ERROR", $"Failed to retrieve download file: {ex.Message}")));
            }
        }
    }
}
