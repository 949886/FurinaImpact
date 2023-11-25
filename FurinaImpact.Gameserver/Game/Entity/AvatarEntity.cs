using FurinaImpact.Common.Constants;
using FurinaImpact.Common.Data.Binout;
using FurinaImpact.Common.Data.Binout.Ability;
using FurinaImpact.Common.Extensions;
using FurinaImpact.Gameserver.Controllers;
using FurinaImpact.Gameserver.Game.Avatar;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Entity;
internal class AvatarEntity : SceneEntity
{
    public override ProtEntityType EntityType => ProtEntityType.Avatar;

    public uint Uid { get; }
    public GameAvatar GameAvatar { get; }

    public AvatarEntity(GameAvatar gameAvatar, uint uid, uint entityId) : base(entityId)
    {
        Uid = uid;
        GameAvatar = gameAvatar;
        Properties = gameAvatar.Properties;
        FightProperties = gameAvatar.FightProperties;
    }

    public AbilityControlBlock BuildAbilityControlBlock(BinDataCollection binData)
    {
        AbilityControlBlock abilityControlBlock = new();
        AvatarConfig avatarConfig = binData.GetAvatarConfig(GameAvatar.AvatarId);

        uint defaultOverrideHash = "Default".GetStableHash();
        foreach (string abilityName in AvatarConstants.CommonAbilities)
        {
            abilityControlBlock.AbilityEmbryoList.Add(new AbilityEmbryo
            {
                AbilityId = (uint)(abilityControlBlock.AbilityEmbryoList.Count + 1),
                AbilityNameHash = abilityName.GetStableHash(),
                AbilityOverrideNameHash = defaultOverrideHash
            });
        }

        foreach (AbilityData ability in avatarConfig.Abilities)
        {
            abilityControlBlock.AbilityEmbryoList.Add(new AbilityEmbryo
            {
                AbilityId = (uint)(abilityControlBlock.AbilityEmbryoList.Count + 1),
                AbilityNameHash = ability.AbilityName.GetStableHash(),
                AbilityOverrideNameHash = ability.GetAbilityOverride().GetStableHash()
            });
        }

        return abilityControlBlock;
    }

    public override SceneEntityInfo AsInfo()
    {
        SceneEntityInfo info = base.AsInfo();

        info.Avatar = new()
        {
            Uid = Uid,
            AvatarId = GameAvatar.AvatarId,
            Guid = GameAvatar.Guid,
            PeerId = 1,
            EquipIdList = { GameAvatar.WeaponId },
            SkillDepotId = GameAvatar.SkillDepotId,
            Weapon = new SceneWeaponInfo
            {
                EntityId = SceneController.WeaponEntityId,
                GadgetId = 50000000 + GameAvatar.WeaponId,
                ItemId = GameAvatar.WeaponId,
                Guid = GameAvatar.WeaponGuid,
                Level = 1,
                PromoteLevel = 0,
                AbilityInfo = new()
            },
            CoreProudSkillLevel = 0,
            InherentProudSkillList = { 832301 },
            SkillLevelMap =
            {
                { 10832, 1 },
                { 10835, 1 },
                { 10831, 1 }
            },
            ProudSkillExtraLevelMap =
            {
                { 8331, 0 },
                { 8332, 0 },
                { 8339, 0 }
            },
            TeamResonanceList = { 10301 },
            WearingFlycloakId = GameAvatar.WearingFlycloakId,
            BornTime = GameAvatar.BornTime,
            CostumeId = 0,
            AnimHash = 0
        };

        return info;
    }
}
