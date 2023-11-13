using System.Text.Json.Serialization;
using FurinaImpact.Common.Data.Binout.Ability;

namespace FurinaImpact.Common.Data.Binout;
public class AvatarConfig
{
    [JsonPropertyName("abilities")]
    public List<AbilityData> Abilities { get; set; }

    public AvatarConfig()
    {
        Abilities = new();
    }
}
