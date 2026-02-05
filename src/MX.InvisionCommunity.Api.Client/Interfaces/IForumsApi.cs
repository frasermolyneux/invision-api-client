using MX.InvisionCommunity.Api.Client.Models;

namespace MX.InvisionCommunity.Api.Client.Interfaces
{
    public interface IForumsApi
    {
        Task<PostTopicResult?> PostTopic(int forumId, int authorId, string title, string post, string prefix);
        Task UpdateTopic(int topicId, int authorId, string post);
    }
}