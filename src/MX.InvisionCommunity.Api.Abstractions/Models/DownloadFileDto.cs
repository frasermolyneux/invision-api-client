using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class DownloadFileDto
    {
        [JsonProperty("id")] public long Id { get; internal set; }
        [JsonProperty("title")] public string? Title { get; internal set; }
        [JsonProperty("category")] public CategoryDto? Category { get; internal set; }
        [JsonProperty("author")] public AuthorDto? Author { get; internal set; }
        [JsonProperty("date")] public DateTimeOffset Date { get; internal set; }
        [JsonProperty("description")] public string? Description { get; internal set; }
        [JsonProperty("version")] public string? Version { get; internal set; }
        [JsonProperty("changelog")] public string? Changelog { get; internal set; }
        [JsonProperty("files")] public FileDto[]? Files { get; internal set; }
        [JsonProperty("screenshots")] public object[]? Screenshots { get; internal set; }
        [JsonProperty("primaryScreenshot")] public object? PrimaryScreenshot { get; internal set; }
        [JsonProperty("downloads")] public long Downloads { get; internal set; }
        [JsonProperty("comments")] public long Comments { get; internal set; }
        [JsonProperty("reviews")] public long Reviews { get; internal set; }
        [JsonProperty("views")] public long Views { get; internal set; }
        [JsonProperty("prefix")] public object? Prefix { get; internal set; }
        [JsonProperty("tags")] public object[]? Tags { get; internal set; }
        [JsonProperty("locked")] public bool Locked { get; internal set; }
        [JsonProperty("hidden")] public bool Hidden { get; internal set; }
        [JsonProperty("pinned")] public bool Pinned { get; internal set; }
        [JsonProperty("featured")] public bool Featured { get; internal set; }
        [JsonProperty("url")] public Uri? Url { get; internal set; }
        [JsonProperty("topic")] public object? Topic { get; internal set; }
    }
}
