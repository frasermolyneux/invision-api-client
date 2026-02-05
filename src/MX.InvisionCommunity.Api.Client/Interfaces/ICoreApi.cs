using MX.InvisionCommunity.Api.Client.Models;

namespace MX.InvisionCommunity.Api.Client.Interfaces
{
    public interface ICoreApi
    {
        Task<Member?> GetMember(string id);
        Task<CoreHello?> GetCoreHello();
    }
}