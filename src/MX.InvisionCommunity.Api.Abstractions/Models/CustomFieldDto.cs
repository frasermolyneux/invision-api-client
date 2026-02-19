using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class CustomFieldDto
    {
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("fields")] public Dictionary<string, FieldDto> Fields { get; internal set; } = new();
    }
}
