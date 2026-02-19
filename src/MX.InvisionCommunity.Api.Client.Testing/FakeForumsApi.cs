using System.Collections.Concurrent;
using System.Net;

using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Testing;

public class FakeForumsApi : IForumsApi
{
    private readonly ConcurrentDictionary<string, (HttpStatusCode StatusCode, ApiError Error)> _postTopicErrors = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<int, (HttpStatusCode StatusCode, ApiError Error)> _updateTopicErrors = new();
    private readonly ConcurrentBag<PostedTopicRecord> _postedTopics = [];
    private readonly ConcurrentBag<UpdatedTopicRecord> _updatedTopics = [];
    private int _nextTopicId = 1000;
    private int _nextPostId = 2000;

    public FakeForumsApi AddPostTopicErrorResponse(string title, HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        _postTopicErrors[title] = (statusCode, new ApiError(errorCode, errorMessage));
        return this;
    }

    public FakeForumsApi AddUpdateTopicErrorResponse(int topicId, HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        _updateTopicErrors[topicId] = (statusCode, new ApiError(errorCode, errorMessage));
        return this;
    }

    public FakeForumsApi Reset()
    {
        _postTopicErrors.Clear();
        _updateTopicErrors.Clear();
        while (_postedTopics.TryTake(out _)) { }
        while (_updatedTopics.TryTake(out _)) { }
        _nextTopicId = 1000;
        _nextPostId = 2000;
        return this;
    }

    public IReadOnlyCollection<PostedTopicRecord> PostedTopics => _postedTopics.ToArray();
    public IReadOnlyCollection<UpdatedTopicRecord> UpdatedTopics => _updatedTopics.ToArray();

    public Task<ApiResult<PostTopicResultDto>> PostTopic(int forumId, int authorId, string title, string post, string prefix, CancellationToken cancellationToken = default)
    {
        _postedTopics.Add(new PostedTopicRecord(forumId, authorId, title, post, prefix));

        if (_postTopicErrors.TryGetValue(title, out var error))
        {
            return Task.FromResult(new ApiResult<PostTopicResultDto>(error.StatusCode,
                new ApiResponse<PostTopicResultDto>(error.Error)));
        }

        var result = InvisionDtoFactory.CreatePostTopicResult(
            topicId: Interlocked.Increment(ref _nextTopicId),
            firstPostId: Interlocked.Increment(ref _nextPostId));

        return Task.FromResult(new ApiResult<PostTopicResultDto>(HttpStatusCode.OK, new ApiResponse<PostTopicResultDto>(result)));
    }

    public Task<ApiResult> UpdateTopic(int topicId, int authorId, string post, CancellationToken cancellationToken = default)
    {
        _updatedTopics.Add(new UpdatedTopicRecord(topicId, authorId, post));

        if (_updateTopicErrors.TryGetValue(topicId, out var error))
        {
            return Task.FromResult(new ApiResult(error.StatusCode, new ApiResponse(error.Error)));
        }

        return Task.FromResult(new ApiResult(HttpStatusCode.OK, new ApiResponse()));
    }
}

public record PostedTopicRecord(int ForumId, int AuthorId, string Title, string Post, string Prefix);
public record UpdatedTopicRecord(int TopicId, int AuthorId, string Post);
