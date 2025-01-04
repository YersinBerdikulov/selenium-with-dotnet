using System.Text.Json.Serialization;

namespace SeleniumWebDriverPracticeWithDotNet;

public sealed class UserData
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}