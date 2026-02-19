using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class CategoryDto
    {
        [JsonProperty("id")] public long Id { get; internal set; }
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("url")] public Uri? Url { get; internal set; }
        [JsonProperty("class")] public string? Class { get; internal set; }
        [JsonProperty("permissions")] public PermissionsDto? Permissions { get; internal set; }
    }
}
