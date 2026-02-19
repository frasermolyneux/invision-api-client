using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Abstractions.Interfaces
{
    public interface IForumsApi
    {
        Task<ApiResult<PostTopicResultDto>> PostTopic(int forumId, int authorId, string title, string post, string prefix, CancellationToken cancellationToken = default);
        Task<ApiResult> UpdateTopic(int topicId, int authorId, string post, CancellationToken cancellationToken = default);
    }
}
