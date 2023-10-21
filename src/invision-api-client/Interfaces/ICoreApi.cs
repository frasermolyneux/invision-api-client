using XtremeIdiots.InvisionCommunity.Models;

namespace XtremeIdiots.InvisionCommunity.Interfaces
{
    public interface ICoreApi
    {
        Task<Member?> GetMember(string id);
        Task<CoreHello?> GetCoreHello();
    }
}