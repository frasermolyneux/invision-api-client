using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Testing;

public static class InvisionDtoFactory
{
    public static CoreHelloDto CreateCoreHello(
        string? communityName = "Test Community",
        string? communityUrl = "https://community.example.com",
        string? ipsVersion = "4.7.0")
    {
        return new CoreHelloDto
        {
            CommunityName = communityName,
            CommunityUrl = communityUrl,
            IpsVersion = ipsVersion
        };
    }

    public static MemberDto CreateMember(
        long id = 1,
        string? name = "Test User",
        string? title = null,
        string? timeZone = "UTC",
        string? formattedName = null,
        GroupDto? primaryGroup = null,
        GroupDto[]? secondaryGroups = null,
        string? email = "test@example.com",
        DateTimeOffset? joined = null,
        string? registrationIpAddress = null,
        long warningPoints = 0,
        long reputationPoints = 0,
        string? photoUrl = null,
        bool photoUrlIsDefault = true,
        string? coverPhotoUrl = null,
        Uri? profileUrl = null,
        bool validating = false,
        long posts = 0,
        DateTimeOffset? lastActivity = null,
        DateTimeOffset? lastVisit = null,
        DateTimeOffset? lastPost = null,
        long profileViews = 0,
        string? birthday = null)
    {
        return new MemberDto
        {
            Id = id,
            Name = name,
            Title = title,
            TimeZone = timeZone,
            FormattedName = formattedName ?? name,
            PrimaryGroup = primaryGroup ?? CreateGroup(),
            SecondaryGroups = secondaryGroups ?? [],
            Email = email,
            Joined = joined ?? DateTimeOffset.UtcNow,
            RegistrationIpAddress = registrationIpAddress,
            WarningPoints = warningPoints,
            ReputationPoints = reputationPoints,
            PhotoUrl = photoUrl,
            PhotoUrlIsDefault = photoUrlIsDefault,
            CoverPhotoUrl = coverPhotoUrl,
            ProfileUrl = profileUrl,
            Validating = validating,
            Posts = posts,
            LastActivity = lastActivity,
            LastVisit = lastVisit,
            LastPost = lastPost,
            ProfileViews = profileViews,
            Birthday = birthday
        };
    }

    public static GroupDto CreateGroup(
        long id = 1,
        string? name = "Members",
        string? formattedName = "Members")
    {
        return new GroupDto
        {
            Id = id,
            Name = name,
            FormattedName = formattedName
        };
    }

    public static AuthorDto CreateAuthor(
        long id = 1,
        string? name = "Test Author",
        string? email = "author@example.com",
        GroupDto? primaryGroup = null)
    {
        return new AuthorDto
        {
            Id = id,
            Name = name,
            FormattedName = name,
            Email = email,
            PrimaryGroup = primaryGroup ?? CreateGroup(),
            SecondaryGroups = [],
            Joined = DateTimeOffset.UtcNow,
            PhotoUrlIsDefault = true
        };
    }

    public static DownloadFileDto CreateDownloadFile(
        long id = 1,
        string? title = "Test Download",
        CategoryDto? category = null,
        AuthorDto? author = null,
        string? description = "Test download description",
        string? version = "1.0.0",
        long downloads = 0,
        long comments = 0,
        long reviews = 0,
        long views = 0)
    {
        return new DownloadFileDto
        {
            Id = id,
            Title = title,
            Category = category ?? CreateCategory(),
            Author = author ?? CreateAuthor(),
            Date = DateTimeOffset.UtcNow,
            Description = description,
            Version = version,
            Files = [],
            Screenshots = [],
            Downloads = downloads,
            Comments = comments,
            Reviews = reviews,
            Views = views,
            Tags = []
        };
    }

    public static CategoryDto CreateCategory(
        long id = 1,
        string? name = "Test Category",
        Uri? url = null)
    {
        return new CategoryDto
        {
            Id = id,
            Name = name,
            Url = url
        };
    }

    public static PostTopicResultDto CreatePostTopicResult(
        int topicId = 1,
        int firstPostId = 1)
    {
        return new PostTopicResultDto
        {
            TopicId = topicId,
            FirstPostId = firstPostId
        };
    }

    public static FieldDto CreateField(
        string? name = "Test Field",
        string? value = "Test Value")
    {
        return new FieldDto
        {
            Name = name,
            Value = value
        };
    }

    public static CustomFieldDto CreateCustomField(
        string? name = "Test Custom Field")
    {
        return new CustomFieldDto
        {
            Name = name
        };
    }

    public static FileDto CreateFile(
        string? name = "test-file.zip",
        string? url = "https://example.com/files/test-file.zip",
        long size = 1024)
    {
        return new FileDto
        {
            Name = name,
            Url = url,
            Size = size
        };
    }

    public static PermissionsDto CreatePermissions(
        long permId = 1,
        string? permView = "*")
    {
        return new PermissionsDto
        {
            PermId = permId,
            PermView = permView
        };
    }
}
