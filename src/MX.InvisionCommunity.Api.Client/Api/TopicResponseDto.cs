using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Client.Api
{
    /// <summary>
    /// Represents the raw topic response from the Invision Community API.
    /// Used internally for deserialization before mapping to PostTopicResultDto.
    /// </summary>
    internal class TopicResponseDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("firstPost")] public FirstPostDto? FirstPost { get; set; }
    }

    internal class FirstPostDto
    {
        [JsonProperty("id")] public int Id { get; set; }
    }
}
