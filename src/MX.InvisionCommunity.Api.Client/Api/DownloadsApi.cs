using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RestSharp;

using MX.InvisionCommunity.Api.Client.Interfaces;
using MX.InvisionCommunity.Api.Client.Models;

namespace MX.InvisionCommunity.Api.Client.Api
{
    public class DownloadsApi : BaseApi, IDownloadsApi
    {
        public DownloadsApi(ILogger<DownloadsApi> logger, IOptions<InvisionApiClientOptions> options, TelemetryClient telemetryClient) : base(logger, options, telemetryClient)
        {
        }

        public async Task<DownloadFile?> GetDownloadFile(int fileId)
        {
            var request = CreateRequest($"api/downloads/files/{fileId}", Method.Get);

            var response = await ExecuteAsync(request);

            if (response.Content != null)
                return JsonConvert.DeserializeObject<DownloadFile>(response.Content);
            else
                throw new Exception($"Response of {request.Method} to '{request.Resource}' has no content");
        }
    }
}
