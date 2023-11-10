using System.Text.Json.Serialization;

namespace FurinaImpact.SDK.Models;

public record AuthAccountData
{
    [JsonPropertyName("area_code")]
    public string? AreaCode { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("is_email_verify")]
    public string? IsEmailVerify { get; set; }

    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("uid")]
    public string? Uid { get; set; }
}
