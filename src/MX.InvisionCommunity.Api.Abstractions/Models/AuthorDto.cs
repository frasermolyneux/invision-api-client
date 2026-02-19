using Newtonsoft.Json;

namespace MX.InvisionCommunity.Api.Abstractions.Models
{
    public class AuthorDto
    {
        [JsonProperty("id")] public long Id { get; internal set; }
        [JsonProperty("name")] public string? Name { get; internal set; }
        [JsonProperty("title")] public string? Title { get; internal set; }
        [JsonProperty("timeZone")] public string? TimeZone { get; internal set; }
        [JsonProperty("formattedName")] public string? FormattedName { get; internal set; }
        [JsonProperty("primaryGroup")] public GroupDto? PrimaryGroup { get; internal set; }
        [JsonProperty("secondaryGroups")] public GroupDto[]? SecondaryGroups { get; internal set; }
        [JsonProperty("email")] public string? Email { get; internal set; }
        [JsonProperty("joined")] public DateTimeOffset Joined { get; internal set; }
        [JsonProperty("registrationIpAddress")] public string? RegistrationIpAddress { get; internal set; }
        [JsonProperty("warningPoints")] public long WarningPoints { get; internal set; }
        [JsonProperty("reputationPoints")] public long ReputationPoints { get; internal set; }
        [JsonProperty("photoUrl")] public string? PhotoUrl { get; internal set; }
        [JsonProperty("photoUrlIsDefault")] public bool PhotoUrlIsDefault { get; internal set; }
        [JsonProperty("coverPhotoUrl")] public string? CoverPhotoUrl { get; internal set; }
        [JsonProperty("profileUrl")] public Uri? ProfileUrl { get; internal set; }
        [JsonProperty("validating")] public bool Validating { get; internal set; }
        [JsonProperty("posts")] public long Posts { get; internal set; }
        [JsonProperty("lastActivity")] public DateTimeOffset LastActivity { get; internal set; }
        [JsonProperty("lastVisit")] public DateTimeOffset LastVisit { get; internal set; }
        [JsonProperty("lastPost")] public DateTimeOffset LastPost { get; internal set; }
        [JsonProperty("profileViews")] public long ProfileViews { get; internal set; }
        [JsonProperty("birthday")] public string? Birthday { get; internal set; }
        [JsonProperty("customFields")] public Dictionary<string, CustomFieldDto> CustomFields { get; internal set; } = new();
    }
}
