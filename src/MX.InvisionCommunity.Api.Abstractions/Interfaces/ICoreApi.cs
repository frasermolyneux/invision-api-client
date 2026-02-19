using MX.Api.Abstractions;
using MX.InvisionCommunity.Api.Abstractions.Models;

namespace MX.InvisionCommunity.Api.Abstractions.Interfaces
{
    public interface ICoreApi
    {
        Task<ApiResult<CoreHelloDto>> GetCoreHello(CancellationToken cancellationToken = default);
        Task<ApiResult<MemberDto>> GetMember(string id, CancellationToken cancellationToken = default);
    }
}
