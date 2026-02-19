using MX.InvisionCommunity.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;

namespace MX.InvisionCommunity.Api.Client.Testing;

public class FakeInvisionApiClient : IInvisionApiClient
{
    public FakeCoreApi CoreApi { get; } = new();
    public FakeDownloadsApi DownloadsApi { get; } = new();
    public FakeForumsApi ForumsApi { get; } = new();

    public ICoreApi Core => CoreApi;
    public IDownloadsApi Downloads => DownloadsApi;
    public IForumsApi Forums => ForumsApi;

    public FakeInvisionApiClient Reset()
    {
        CoreApi.Reset();
        DownloadsApi.Reset();
        ForumsApi.Reset();
        return this;
    }
}
