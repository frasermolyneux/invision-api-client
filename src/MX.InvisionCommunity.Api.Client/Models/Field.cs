using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Client.Models
{
    public class Field
    {
        [JsonProperty("name")] public string? Name { get; set; }
        [JsonProperty("value")] public string? Value { get; set; }
    }
}