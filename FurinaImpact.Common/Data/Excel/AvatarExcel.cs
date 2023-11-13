using System.Text.Json.Serialization;
using FurinaImpact.Common.Data.Excel.Attributes;

namespace FurinaImpact.Common.Data.Excel;

[Excel(ExcelType.Avatar, "AvatarExcelConfigData.json")]
public class AvatarExcel : ExcelItem
{
    // TODO: implement enums for some fields!

    public override uint ExcelId => Id;

    [JsonPropertyName("attackBase")]
    public double AttackBase { get; set; }

    [JsonPropertyName("avatarIdentityType")]
    public string? AvatarIdentityType { get; set; }

    [JsonPropertyName("avatarPromoteId")]
    public uint AvatarPromoteId { get; set; }

    [JsonPropertyName("avatarPromoteRewardIdList")]
    public List<int> AvatarPromoteRewardIdList { get; set; }

    [JsonPropertyName("avatarPromoteRewardLevelList")]
    public List<int> AvatarPromoteRewardLevelList { get; set; }

    [JsonPropertyName("bodyType")]
    public required string BodyType { get; set; }

    [JsonPropertyName("iconName")]
    public required string IconName { get; set; }

    [JsonPropertyName("chargeEfficiency")]
    public double ChargeEfficiency { get; set; }

    [JsonPropertyName("combatConfigHashSuffix")]
    public ulong CombatConfigHashSuffix { get; set; }

    [JsonPropertyName("controllerPathHashSuffix")]
    public ulong ControllerPathHashSuffix { get; set; }

    [JsonPropertyName("controllerPathRemoteHashSuffix")]
    public ulong ControllerPathRemoteHashSuffix { get; set; }

    [JsonPropertyName("coopPicNameHashSuffix")]
    public ulong CoopPicNameHashSuffix { get; set; }

    [JsonPropertyName("critical")]
    public double Critical { get; set; }

    [JsonPropertyName("criticalHurt")]
    public double CriticalHurt { get; set; }

    [JsonPropertyName("defenseBase")]
    public double DefenseBase { get; set; }

    [JsonPropertyName("descTextMapHash")]
    public ulong DescTextMapHash { get; set; }

    [JsonPropertyName("featureTagGroupId")]
    public uint FeatureTagGroupId { get; set; }

    [JsonPropertyName("gachaCardNameHashSuffix")]
    public ulong GachaCardNameHashSuffix { get; set; }

    [JsonPropertyName("gachaImageNameHashSuffix")]
    public ulong GachaImageNameHashSuffix { get; set; }

    [JsonPropertyName("hpBase")]
    public double HpBase { get; set; }

    [JsonPropertyName("id")]
    public uint Id { get; set; }

    [JsonPropertyName("infoDesc")]
    public long InfoDesc { get; set; }

    [JsonPropertyName("initialWeapon")]
    public uint InitialWeapon { get; set; }

    [JsonPropertyName("manekinJsonConfigHashSuffix")]
    public long ManekinJsonConfigHashSuffix { get; set; }

    [JsonPropertyName("manekinMotionConfig")]
    public uint ManekinMotionConfig { get; set; }

    [JsonPropertyName("manekinPathHashSuffix")]
    public ulong ManekinPathHashSuffix { get; set; }

    [JsonPropertyName("nameTextMapHash")]
    public ulong NameTextMapHash { get; set; }

    [JsonPropertyName("prefabPathHashSuffix")]
    public ulong PrefabPathHashSuffix { get; set; }

    [JsonPropertyName("prefabPathRagdollHashSuffix")]
    public ulong PrefabPathRagdollHashSuffix { get; set; }

    [JsonPropertyName("prefabPathRemoteHashSuffix")]
    public ulong PrefabPathRemoteHashSuffix { get; set; }

    [JsonPropertyName("propGrowCurves")]
    public List<PropGrowCurve> PropGrowCurves { get; set; }

    [JsonPropertyName("qualityType")]
    public required string QualityType { get; set; }

    [JsonPropertyName("skillDepotId")]
    public uint SkillDepotId { get; set; }

    [JsonPropertyName("staminaRecoverSpeed")]
    public double StaminaRecoverSpeed { get; set; }

    [JsonPropertyName("useType")]
    public string? UseType { get; set; }

    [JsonPropertyName("weaponType")]
    public required string WeaponType { get; set; }

    public AvatarExcel()
    {
        AvatarPromoteRewardIdList = new();
        AvatarPromoteRewardLevelList = new();
        PropGrowCurves = new();
    }
}

public class PropGrowCurve
{
    [JsonPropertyName("growCurve")]
    public required string GrowCurve { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }
}