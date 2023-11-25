using System.Collections.Immutable;

namespace FurinaImpact.Common.Constants;
public static class AvatarConstants
{
    public static readonly ImmutableArray<string> CommonAbilities = ImmutableArray.Create
    (
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
    );
}
