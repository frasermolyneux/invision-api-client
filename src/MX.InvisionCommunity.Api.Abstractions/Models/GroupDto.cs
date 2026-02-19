using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class GroupDto
    {
        [JsonProperty("id")] public long Id { get; internal set; }
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("formattedName")] public string? FormattedName { get; internal set; }
    }
}
