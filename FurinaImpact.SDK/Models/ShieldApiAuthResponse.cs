using System.Text.Json.Serialization;

namespace FurinaImpact.SDK.Models;

public record ShieldApiAuthResponse
{
    [JsonPropertyName("data")]
    public AuthSuccessData? Data { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "OK";

    [JsonPropertyName("retcode")]
    public int Retcode { get; set; }
}
