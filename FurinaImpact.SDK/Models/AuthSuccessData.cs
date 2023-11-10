using System.Text.Json.Serialization;

namespace FurinaImpact.SDK.Models;

public record AuthSuccessData
{
    [JsonPropertyName("account")]
    public AuthAccountData? Account { get; set; }

    [JsonPropertyName("device_grant_required")]
    public bool DeviceGrantRequired { get; set; }

    [JsonPropertyName("reactivate_required")]
    public bool ReactivateRequired { get; set; }

    [JsonPropertyName("realperson_required")]
    public bool RealPersonRequired { get; set; }

    [JsonPropertyName("safe_mobile_required")]
    public bool SafeMobileRequired { get; set; }
}
