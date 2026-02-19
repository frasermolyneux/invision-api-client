using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class PermissionsDto
    {
        [JsonProperty("perm_id")] public long PermId { get; internal set; }
        [JsonProperty("perm_view")] public string? PermView { get; internal set; }
        [JsonProperty("perm_2")] public string? Perm2 { get; internal set; }
        [JsonProperty("perm_3")] public string? Perm3 { get; internal set; }
        [JsonProperty("perm_4")] public string? Perm4 { get; internal set; }
        [JsonProperty("perm_5")] public string? Perm5 { get; internal set; }
        [JsonProperty("perm_6")] public string? Perm6 { get; internal set; }
        [JsonProperty("perm_7")] public string? Perm7 { get; internal set; }
    }
}
