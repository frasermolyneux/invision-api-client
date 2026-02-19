using MX.InvisionCommunity.Api.Client.Testing;

namespace MX.InvisionCommunity.Api.Client.Testing.Tests;

public class InvisionDtoFactoryTests
{
    [Fact]
    public void CreateCoreHello_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateCoreHello();

        Assert.Equal("Test Community", dto.CommunityName);
        Assert.Equal("https://community.example.com", dto.CommunityUrl);
        Assert.Equal("4.7.0", dto.IpsVersion);
    }

    [Fact]
    public void CreateCoreHello_AcceptsCustomValues()
    {
        var dto = InvisionDtoFactory.CreateCoreHello(communityName: "Custom", communityUrl: "https://custom.com", ipsVersion: "5.0.0");

        Assert.Equal("Custom", dto.CommunityName);
        Assert.Equal("https://custom.com", dto.CommunityUrl);
        Assert.Equal("5.0.0", dto.IpsVersion);
    }

    [Fact]
    public void CreateMember_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateMember();

        Assert.Equal(1, dto.Id);
        Assert.Equal("Test User", dto.Name);
        Assert.Equal("test@example.com", dto.Email);
        Assert.Equal("UTC", dto.TimeZone);
        Assert.NotNull(dto.PrimaryGroup);
        Assert.NotNull(dto.SecondaryGroups);
    }

    [Fact]
    public void CreateMember_AcceptsCustomValues()
    {
        var dto = InvisionDtoFactory.CreateMember(id: 42, name: "John", email: "john@test.com", posts: 100);

        Assert.Equal(42, dto.Id);
        Assert.Equal("John", dto.Name);
        Assert.Equal("john@test.com", dto.Email);
        Assert.Equal(100, dto.Posts);
    }

    [Fact]
    public void CreateGroup_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateGroup();

        Assert.Equal(1, dto.Id);
        Assert.Equal("Members", dto.Name);
        Assert.Equal("Members", dto.FormattedName);
    }

    [Fact]
    public void CreateAuthor_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateAuthor();

        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Author", dto.Name);
        Assert.Equal("author@example.com", dto.Email);
        Assert.NotNull(dto.PrimaryGroup);
    }

    [Fact]
    public void CreateDownloadFile_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateDownloadFile();

        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Download", dto.Title);
        Assert.Equal("1.0.0", dto.Version);
        Assert.NotNull(dto.Category);
        Assert.NotNull(dto.Author);
    }

    [Fact]
    public void CreateDownloadFile_AcceptsCustomValues()
    {
        var dto = InvisionDtoFactory.CreateDownloadFile(id: 99, title: "Custom", downloads: 500);

        Assert.Equal(99, dto.Id);
        Assert.Equal("Custom", dto.Title);
        Assert.Equal(500, dto.Downloads);
    }

    [Fact]
    public void CreateCategory_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateCategory();

        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Category", dto.Name);
    }

    [Fact]
    public void CreatePostTopicResult_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreatePostTopicResult();

        Assert.Equal(1, dto.TopicId);
        Assert.Equal(1, dto.FirstPostId);
    }

    [Fact]
    public void CreatePostTopicResult_AcceptsCustomValues()
    {
        var dto = InvisionDtoFactory.CreatePostTopicResult(topicId: 42, firstPostId: 99);

        Assert.Equal(42, dto.TopicId);
        Assert.Equal(99, dto.FirstPostId);
    }

    [Fact]
    public void CreateField_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateField();

        Assert.Equal("Test Field", dto.Name);
        Assert.Equal("Test Value", dto.Value);
    }

    [Fact]
    public void CreateCustomField_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateCustomField();

        Assert.Equal("Test Custom Field", dto.Name);
        Assert.Empty(dto.Fields);
    }

    [Fact]
    public void CreateFile_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreateFile();

        Assert.Equal("test-file.zip", dto.Name);
        Assert.Equal(1024, dto.Size);
    }

    [Fact]
    public void CreatePermissions_ReturnsDefaults()
    {
        var dto = InvisionDtoFactory.CreatePermissions();

        Assert.Equal(1, dto.PermId);
        Assert.Equal("*", dto.PermView);
    }
}
