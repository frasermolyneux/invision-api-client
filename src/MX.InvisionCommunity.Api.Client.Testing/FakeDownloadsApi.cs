using System.Collections.Concurrent;
using System.Net;

using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Testing;

public class FakeDownloadsApi : IDownloadsApi
{
    private readonly ConcurrentDictionary<int, DownloadFileDto> _responses = new();
    private readonly ConcurrentDictionary<int, (HttpStatusCode StatusCode, ApiError Error)> _errorResponses = new();
    private readonly ConcurrentBag<int> _lookedUpFiles = [];
    private DefaultFakeBehavior _defaultBehavior = DefaultFakeBehavior.ReturnGenericSuccess;
    private HttpStatusCode _defaultErrorStatusCode = HttpStatusCode.NotFound;
    private string _defaultErrorCode = "NOT_FOUND";
    private string _defaultErrorMessage = "Download file not configured in fake";

    public FakeDownloadsApi AddResponse(int fileId, DownloadFileDto dto)
    {
        _responses[fileId] = dto;
        return this;
    }

    public FakeDownloadsApi AddErrorResponse(int fileId, HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        _errorResponses[fileId] = (statusCode, new ApiError(errorCode, errorMessage));
        return this;
    }

    public FakeDownloadsApi SetDefaultBehavior(
        DefaultFakeBehavior behavior,
        HttpStatusCode errorStatusCode = HttpStatusCode.NotFound,
        string errorCode = "NOT_FOUND",
        string errorMessage = "Download file not configured in fake")
    {
        _defaultBehavior = behavior;
        _defaultErrorStatusCode = errorStatusCode;
        _defaultErrorCode = errorCode;
        _defaultErrorMessage = errorMessage;
        return this;
    }

    public FakeDownloadsApi Reset()
    {
        _responses.Clear();
        _errorResponses.Clear();
        while (_lookedUpFiles.TryTake(out _)) { }
        _defaultBehavior = DefaultFakeBehavior.ReturnGenericSuccess;
        _defaultErrorStatusCode = HttpStatusCode.NotFound;
        _defaultErrorCode = "NOT_FOUND";
        _defaultErrorMessage = "Download file not configured in fake";
        return this;
    }

    public IReadOnlyCollection<int> LookedUpFiles => _lookedUpFiles.ToArray();

    public Task<ApiResult<DownloadFileDto>> GetDownloadFile(int fileId, CancellationToken cancellationToken = default)
    {
        _lookedUpFiles.Add(fileId);

        if (_errorResponses.TryGetValue(fileId, out var error))
        {
            return Task.FromResult(new ApiResult<DownloadFileDto>(error.StatusCode,
                new ApiResponse<DownloadFileDto>(error.Error)));
        }

        if (_responses.TryGetValue(fileId, out var dto))
        {
            return Task.FromResult(new ApiResult<DownloadFileDto>(HttpStatusCode.OK, new ApiResponse<DownloadFileDto>(dto)));
        }

        if (_defaultBehavior == DefaultFakeBehavior.ReturnError)
        {
            return Task.FromResult(new ApiResult<DownloadFileDto>(_defaultErrorStatusCode,
                new ApiResponse<DownloadFileDto>(new ApiError(_defaultErrorCode, _defaultErrorMessage))));
        }

        dto = InvisionDtoFactory.CreateDownloadFile(id: fileId);
        return Task.FromResult(new ApiResult<DownloadFileDto>(HttpStatusCode.OK, new ApiResponse<DownloadFileDto>(dto)));
    }
}
