using System.Net;

using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class FakeForumsApiTests
{
    [Fact]
    public async Task PostTopic_ReturnsSuccessWithAutoIncrementedIds()
    {
        var fake = new FakeForumsApi();

        var result1 = await fake.PostTopic(1, 100, "Title 1", "Body 1", "prefix");
        var result2 = await fake.PostTopic(1, 100, "Title 2", "Body 2", "prefix");

        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.NotEqual(result1.Result?.Data?.TopicId, result2.Result?.Data?.TopicId);
    }

    [Fact]
    public async Task PostTopic_TracksPostedTopics()
    {
        var fake = new FakeForumsApi();

        await fake.PostTopic(1, 100, "Title", "Body", "prefix");

        Assert.Single(fake.PostedTopics);
        var posted = fake.PostedTopics.First();
        Assert.Equal(1, posted.ForumId);
        Assert.Equal(100, posted.AuthorId);
        Assert.Equal("Title", posted.Title);
        Assert.Equal("Body", posted.Post);
        Assert.Equal("prefix", posted.Prefix);
    }

    [Fact]
    public async Task PostTopic_ReturnsErrorWhenConfigured()
    {
        var fake = new FakeForumsApi();
        fake.AddPostTopicErrorResponse("Bad Title", HttpStatusCode.BadRequest, "INVALID", "Invalid topic");

        var result = await fake.PostTopic(1, 100, "Bad Title", "Body", "prefix");

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task UpdateTopic_ReturnsSuccess()
    {
        var fake = new FakeForumsApi();

        var result = await fake.UpdateTopic(1, 100, "Updated body");

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateTopic_TracksUpdatedTopics()
    {
        var fake = new FakeForumsApi();

        await fake.UpdateTopic(42, 100, "New body");

        Assert.Single(fake.UpdatedTopics);
        var updated = fake.UpdatedTopics.First();
        Assert.Equal(42, updated.TopicId);
        Assert.Equal(100, updated.AuthorId);
        Assert.Equal("New body", updated.Post);
    }

    [Fact]
    public async Task UpdateTopic_ReturnsErrorWhenConfigured()
    {
        var fake = new FakeForumsApi();
        fake.AddUpdateTopicErrorResponse(42, HttpStatusCode.NotFound, "NOT_FOUND", "Topic not found");

        var result = await fake.UpdateTopic(42, 100, "Body");

        Assert.True(result.IsNotFound);
    }

    [Fact]
    public async Task Reset_ClearsAllState()
    {
        var fake = new FakeForumsApi();
        await fake.PostTopic(1, 1, "T", "B", "P");
        await fake.UpdateTopic(1, 1, "B");

        fake.Reset();

        Assert.Empty(fake.PostedTopics);
        Assert.Empty(fake.UpdatedTopics);
    }
}
