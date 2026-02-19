using System.Collections.Concurrent;
using System.Net;

using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Interfaces;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Client.Testing;

public enum DefaultFakeBehavior
{
    ReturnGenericSuccess,
    ReturnError
}

public class FakeCoreApi : ICoreApi
{
    private readonly ConcurrentDictionary<string, MemberDto> _memberResponses = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, (HttpStatusCode StatusCode, ApiError Error)> _memberErrorResponses = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<string> _lookedUpMembers = [];
    private CoreHelloDto? _coreHelloResponse;
    private DefaultFakeBehavior _defaultBehavior = DefaultFakeBehavior.ReturnGenericSuccess;
    private HttpStatusCode _defaultErrorStatusCode = HttpStatusCode.NotFound;
    private string _defaultErrorCode = "NOT_FOUND";
    private string _defaultErrorMessage = "Member not configured in fake";

    public FakeCoreApi AddMemberResponse(string id, MemberDto dto)
    {
        _memberResponses[id] = dto;
        return this;
    }

    public FakeCoreApi AddMemberErrorResponse(string id, HttpStatusCode statusCode, string errorCode, string errorMessage)
    {
        _memberErrorResponses[id] = (statusCode, new ApiError(errorCode, errorMessage));
        return this;
    }

    public FakeCoreApi WithCoreHello(CoreHelloDto dto)
    {
        _coreHelloResponse = dto;
        return this;
    }

    public FakeCoreApi SetDefaultBehavior(
        DefaultFakeBehavior behavior,
        HttpStatusCode errorStatusCode = HttpStatusCode.NotFound,
        string errorCode = "NOT_FOUND",
        string errorMessage = "Member not configured in fake")
    {
        _defaultBehavior = behavior;
        _defaultErrorStatusCode = errorStatusCode;
        _defaultErrorCode = errorCode;
        _defaultErrorMessage = errorMessage;
        return this;
    }

    public FakeCoreApi Reset()
    {
        _memberResponses.Clear();
        _memberErrorResponses.Clear();
        while (_lookedUpMembers.TryTake(out _)) { }
        _coreHelloResponse = null;
        _defaultBehavior = DefaultFakeBehavior.ReturnGenericSuccess;
        _defaultErrorStatusCode = HttpStatusCode.NotFound;
        _defaultErrorCode = "NOT_FOUND";
        _defaultErrorMessage = "Member not configured in fake";
        return this;
    }

    public IReadOnlyCollection<string> LookedUpMembers => _lookedUpMembers.ToArray();

    public Task<ApiResult<CoreHelloDto>> GetCoreHello(CancellationToken cancellationToken = default)
    {
        var dto = _coreHelloResponse ?? InvisionDtoFactory.CreateCoreHello();
        return Task.FromResult(new ApiResult<CoreHelloDto>(HttpStatusCode.OK, new ApiResponse<CoreHelloDto>(dto)));
    }

    public Task<ApiResult<MemberDto>> GetMember(string id, CancellationToken cancellationToken = default)
    {
        _lookedUpMembers.Add(id);

        if (_memberErrorResponses.TryGetValue(id, out var error))
        {
            return Task.FromResult(new ApiResult<MemberDto>(error.StatusCode,
                new ApiResponse<MemberDto>(error.Error)));
        }

        if (_memberResponses.TryGetValue(id, out var dto))
        {
            return Task.FromResult(new ApiResult<MemberDto>(HttpStatusCode.OK, new ApiResponse<MemberDto>(dto)));
        }

        if (_defaultBehavior == DefaultFakeBehavior.ReturnError)
        {
            return Task.FromResult(new ApiResult<MemberDto>(_defaultErrorStatusCode,
                new ApiResponse<MemberDto>(new ApiError(_defaultErrorCode, _defaultErrorMessage))));
        }

        dto = InvisionDtoFactory.CreateMember(id: long.TryParse(id, out var parsedId) ? parsedId : 1, name: $"Test Member {id}");
        return Task.FromResult(new ApiResult<MemberDto>(HttpStatusCode.OK, new ApiResponse<MemberDto>(dto)));
    }
}
