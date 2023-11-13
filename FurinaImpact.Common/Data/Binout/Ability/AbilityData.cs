using System.Text.Json.Serialization;

namespace FurinaImpact.Common.Data.Binout.Ability;
public class AbilityData
{
    [JsonPropertyName("abilityID")]
    public required string AbilityId { get; set; }

    [JsonPropertyName("abilityName")]
    public required string AbilityName { get; set; }

    [JsonPropertyName("abilityOverride")]
    public required string AbilityOverride { get; set; }

    public string GetAbilityOverride()
        => string.IsNullOrEmpty(AbilityOverride) ? "Default" : AbilityOverride;
}
