using System.Collections.Immutable;
using System.Text.Json;
using FurinaImpact.Common.Data.Provider;
using Microsoft.Extensions.Logging;

namespace FurinaImpact.Common.Data.Binout;
public class BinDataCollection
{
    public readonly string[] CommonAbilities =
    {
        "Avatar_DefaultAbility_VisionReplaceDieInvincible",
        "Avatar_DefaultAbility_AvartarInShaderChange",
        "Avatar_SprintBS_Invincible",
        "Avatar_Freeze_Duration_Reducer",
        "Avatar_Attack_ReviveEnergy",
        "Avatar_Component_Initializer",
        "Avatar_FallAnthem_Achievement_Listener",
        "GrapplingHookSkill_Ability",
        "Avatar_PlayerBoy_DiveStamina_Reduction",
        "Ability_Avatar_Dive_Team",
        "Ability_Avatar_Dive_SealEcho",
        "Absorb_SealEcho_Bullet_01",
        "Absorb_SealEcho_Bullet_02",
        "Ability_Avatar_Dive_CrabShield",
        "ActivityAbility_Absorb_Shoot",
        "SceneAbility_DiveVolume"
    };

    private readonly ImmutableDictionary<uint, AvatarConfig> _avatarConfigs;

    public BinDataCollection(IAssetProvider assetProvider, ILogger<BinDataCollection> logger, DataHelper dataHelper)
    {
        _avatarConfigs = LoadAvatarConfigs(assetProvider, dataHelper);

        logger.LogInformation("Loaded {count} avatar configs", _avatarConfigs.Count);
    }

    public AvatarConfig GetAvatarConfig(uint id)
    {
        return _avatarConfigs[id];
    }

    private static ImmutableDictionary<uint, AvatarConfig> LoadAvatarConfigs(IAssetProvider assetProvider, DataHelper dataHelper)
    {
        ImmutableDictionary<uint, AvatarConfig>.Builder builder = ImmutableDictionary.CreateBuilder<uint, AvatarConfig>();
        IEnumerable<string> avatarConfigFiles = assetProvider.EnumerateAvatarConfigFiles();

        foreach (string avatarConfigFile in avatarConfigFiles)
        {
            string avatarName = avatarConfigFile[(avatarConfigFile.LastIndexOf('_') + 1)..];
            avatarName = avatarName.Remove(avatarName.IndexOf('.'));

            if (dataHelper.TryResolveAvatarIdByName(avatarName, out uint id)) 
            {
                JsonDocument configJson = assetProvider.GetFileAsJsonDocument(avatarConfigFile);
                if (configJson.RootElement.ValueKind != JsonValueKind.Object)
                    throw new JsonException($"BinDataCollection::LoadAvatarConfigs - expected an object, got {configJson.RootElement.ValueKind}");

                AvatarConfig avatarConfig = configJson.RootElement.Deserialize<AvatarConfig>()!;
                builder.Add(id, avatarConfig);
            }
            else
            {
                throw new KeyNotFoundException($"BinDataCollection::LoadAvatarConfigs - failed to resolve avatar id for {avatarName}");
            }
        }

        return builder.ToImmutable();
    }
}
