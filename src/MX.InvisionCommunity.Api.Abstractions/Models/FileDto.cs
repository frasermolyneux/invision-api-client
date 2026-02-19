using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class FileDto
    {
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("url")] public string? Url { get; internal set; }
        [JsonProperty("size")] public long Size { get; internal set; }
    }
}
