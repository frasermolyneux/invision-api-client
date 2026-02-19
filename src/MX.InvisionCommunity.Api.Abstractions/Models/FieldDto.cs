using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class FieldDto
    {
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("value")] public string? Value { get; internal set; }
    }
}
