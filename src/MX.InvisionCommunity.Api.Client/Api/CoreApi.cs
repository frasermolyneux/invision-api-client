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
    public class CoreApi : BaseApi<InvisionApiClientOptions>, ICoreApi
    {
        public CoreApi(
            ILogger<BaseApi<InvisionApiClientOptions>> logger,
            IApiTokenProvider? apiTokenProvider,
            IRestClientService restClientService,
            InvisionApiClientOptions options)
            : base(logger, apiTokenProvider, restClientService, options)
        {
        }

        public async Task<ApiResult<CoreHelloDto>> GetCoreHello(CancellationToken cancellationToken = default)
        {
            try
            {
                var request = await CreateRequestAsync("api/core/hello", Method.Get, cancellationToken);
                var response = await ExecuteAsync(request, cancellationToken);

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    return new ApiResult<CoreHelloDto>(response.StatusCode,
                        new ApiResponse<CoreHelloDto>(new ApiError("NO_CONTENT", "Response has no content")));
                }

                var data = JsonConvert.DeserializeObject<CoreHelloDto>(response.Content);
                return new ApiResult<CoreHelloDto>(response.StatusCode, new ApiResponse<CoreHelloDto>(data));
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new ApiResult<CoreHelloDto>(HttpStatusCode.InternalServerError,
                    new ApiResponse<CoreHelloDto>(new ApiError("CLIENT_ERROR", $"Failed to retrieve core hello: {ex.Message}")));
            }
        }

        public async Task<ApiResult<MemberDto>> GetMember(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = await CreateRequestAsync($"api/core/members/{Uri.EscapeDataString(id)}", Method.Get, cancellationToken);
                var response = await ExecuteAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new ApiResult<MemberDto>(HttpStatusCode.NotFound,
                        new ApiResponse<MemberDto>(new ApiError("NOT_FOUND", $"Member '{id}' not found")));
                }

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    return new ApiResult<MemberDto>(response.StatusCode,
                        new ApiResponse<MemberDto>(new ApiError("NO_CONTENT", "Response has no content")));
                }

                var data = JsonConvert.DeserializeObject<MemberDto>(response.Content);
                return new ApiResult<MemberDto>(response.StatusCode, new ApiResponse<MemberDto>(data));
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new ApiResult<MemberDto>(HttpStatusCode.InternalServerError,
                    new ApiResponse<MemberDto>(new ApiError("CLIENT_ERROR", $"Failed to retrieve member: {ex.Message}")));
            }
        }
    }
}
