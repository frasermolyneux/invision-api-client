# MX.InvisionCommunity.Api.Client

REST API client for [Invision Community](https://invisioncommunity.com/) forums. Provides typed access to core member operations, downloads, and forum topic management.

## Installation

```shell
dotnet add package MX.InvisionCommunity.Api.Client
```

## Quick Start

### Register Services

```csharp
builder.Services.AddInvisionApiClient(options =>
{
    options.BaseUrl = "https://your-forum.example.com";
    options.ApiKey = "your-api-key";
});
```

### Inject and Use

```csharp
public class ForumService
{
    private readonly IInvisionApiClient _client;

    public ForumService(IInvisionApiClient client)
    {
        _client = client;
    }

    public async Task<Member?> GetMember(string memberId)
    {
        return await _client.Core.GetMember(memberId);
    }

    public async Task CreateForumPost(int forumId, int authorId, string title, string content)
    {
        await _client.Forums.PostTopic(forumId, authorId, title, content, prefix: "");
    }

    public async Task<DownloadFile?> GetFile(int fileId)
    {
        return await _client.Downloads.GetDownloadFile(fileId);
    }
}
```

## API Surface

The `IInvisionApiClient` exposes:

| Property | Methods |
|----------|---------|
| `Core` | `GetMember(id)`, `GetCoreHello()` |
| `Forums` | `PostTopic(...)`, `UpdateTopic(...)` |
| `Downloads` | `GetDownloadFile(fileId)` |

## Configuration

`InvisionApiClientOptions`:

| Property | Description |
|----------|-------------|
| `BaseUrl` | Base URL of the Invision Community instance |
| `ApiKey` | API key for authentication |
| `ApiPathPrefix` | Optional path prefix for API endpoints |

## License

This project is licensed under the [GPL-3.0-only](https://spdx.org/licenses/GPL-3.0-only.html) license.
