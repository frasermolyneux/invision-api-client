using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class CoreHelloDto
    {
        [JsonProperty("communityName")] public string? CommunityName { get; internal set; }
        [JsonProperty("communityUrl")] public string? CommunityUrl { get; internal set; }
        [JsonProperty("ipsVersion")] public string? IpsVersion { get; internal set; }
    }
}
