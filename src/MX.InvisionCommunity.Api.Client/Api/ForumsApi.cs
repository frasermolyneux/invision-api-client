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
    public class ForumsApi : BaseApi<InvisionApiClientOptions>, IForumsApi
    {
        public ForumsApi(
            ILogger<BaseApi<InvisionApiClientOptions>> logger,
            IApiTokenProvider? apiTokenProvider,
            IRestClientService restClientService,
            InvisionApiClientOptions options)
            : base(logger, apiTokenProvider, restClientService, options)
        {
        }

        public async Task<ApiResult<PostTopicResultDto>> PostTopic(int forumId, int authorId, string title, string post, string prefix, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = await CreateRequestAsync("api/forums/topics", Method.Post, cancellationToken);

                request.AddParameter("forum", forumId);
                request.AddParameter("author", authorId);
                request.AddParameter("title", title);
                request.AddParameter("post", post);
                request.AddParameter("prefix", prefix);

                var response = await ExecuteAsync(request, cancellationToken);

                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    return new ApiResult<PostTopicResultDto>(response.StatusCode,
                        new ApiResponse<PostTopicResultDto>(new ApiError("NO_CONTENT", "Response has no content")));
                }

                var topicResponse = JsonConvert.DeserializeObject<TopicResponseDto>(response.Content);

                if (topicResponse == null)
                {
                    return new ApiResult<PostTopicResultDto>(response.StatusCode,
                        new ApiResponse<PostTopicResultDto>(new ApiError("DESERIALIZATION_ERROR", "Failed to deserialize topic response")));
                }

                var result = new PostTopicResultDto
                {
                    TopicId = topicResponse.Id,
                    FirstPostId = topicResponse.FirstPost?.Id ?? 0
                };

                return new ApiResult<PostTopicResultDto>(response.StatusCode, new ApiResponse<PostTopicResultDto>(result));
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new ApiResult<PostTopicResultDto>(HttpStatusCode.InternalServerError,
                    new ApiResponse<PostTopicResultDto>(new ApiError("CLIENT_ERROR", $"Failed to post topic: {ex.Message}")));
            }
        }

        public async Task<ApiResult> UpdateTopic(int topicId, int authorId, string post, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = await CreateRequestAsync($"api/forums/topics/{topicId}", Method.Post, cancellationToken);

                request.AddParameter("author", authorId);
                request.AddParameter("post", post);

                var response = await ExecuteAsync(request, cancellationToken);

                return new ApiResult(response.StatusCode, new ApiResponse());
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                return new ApiResult(HttpStatusCode.InternalServerError,
                    new ApiResponse(new ApiError("CLIENT_ERROR", $"Failed to update topic: {ex.Message}")));
            }
        }
    }
}
