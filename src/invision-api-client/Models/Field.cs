using Newtonsoft.Json;

namespace XtremeIdiots.InvisionCommunity.Models
{
    public class Field
    {
        [JsonProperty("name")] public string? Name { get; set; }
        [JsonProperty("value")] public string? Value { get; set; }
    }
}