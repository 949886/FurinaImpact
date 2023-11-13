using FurinaImpact.Common.Data.Binout;
using FurinaImpact.Common.Data.Binout.Ability;
using FurinaImpact.Common.Extensions;
using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Game;
using FurinaImpact.Gameserver.Game.Avatar;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class SceneController : ControllerBase
{
    // TODO: Scene management, Entity management!!!

    public const uint EnterSceneToken = 19483;

    private const uint AvatarEntityId = 16777219;
    private const uint WeaponEntityId = 100663300;

    [NetCommand(CmdType.PostEnterSceneReq)]
    public ValueTask<IResult> OnPostEnterSceneReq()
    {
        return ValueTask.FromResult(Response(CmdType.PostEnterSceneRsp, new PostEnterSceneRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }

    [NetCommand(CmdType.EnterSceneDoneReq)]
    public ValueTask<IResult> OnEnterSceneDoneReq(Player player)
    {
        bool furinaExists = player.TryGetAvatar(10000089, out GameAvatar? gameAvatar); // Currently hardcode to 10000089. It's definitely exists because of UnlockAllAvatars()
        if (!furinaExists) throw new InvalidOperationException("Furina doesn't exist? It's FurinaImpact, you should have Furina.");

        SceneEntityInfo avatarEntity = new()
        {
            EntityType = ProtEntityType.Avatar,
            EntityId = AvatarEntityId,
            MotionInfo = new MotionInfo
            {
                Pos = new Vector
                {
                    X = 2336.789f,
                    Y = 249.98996f,
                    Z = -751.3081f
                },
                Rot = new Vector(),
                Speed = new Vector(),
                State = MotionState.None,
            },
            LifeState = 1, // ALIVE
            AnimatorParaList = { new AnimatorParameterValueInfoPair() },
            Avatar = new SceneAvatarInfo
            {
                Uid = player.Uid,
                AvatarId = gameAvatar!.AvatarId,
                Guid = gameAvatar.Guid,
                PeerId = 1,
                EquipIdList = { gameAvatar.WeaponId },
                SkillDepotId = gameAvatar.SkillDepotId,
                Weapon = new SceneWeaponInfo
                {
                    EntityId = WeaponEntityId,
                    GadgetId = 50000000 + gameAvatar.WeaponId,
                    ItemId = gameAvatar.WeaponId,
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
                WearingFlycloakId = gameAvatar.WearingFlycloakId,
                BornTime = gameAvatar.BornTime,
                CostumeId = 0,
                AnimHash = 0
            },
            LastMoveSceneTimeMs = 0,
            LastMoveReliableSeq = 0,
            EntityClientData = new(),
            EntityAuthorityInfo = new EntityAuthorityInfo
            {
                AbilityInfo = new(),
                RendererChangedInfo = new(),
                AiInfo = new()
                {
                    IsAiOpen = true,
                    BornPos = new()
                },
                BornPos = new(),
            },
        };

        foreach (PropValue propValue in gameAvatar.Properties)
        {
            avatarEntity.PropList.Add(new PropPair { Type = propValue.Type, PropValue = propValue });
        }

        foreach (FightPropPair pair in gameAvatar.FightProperties)
        {
            avatarEntity.FightPropList.Add(pair);
        }

        AddNotify(CmdType.SceneEntityAppearNotify, new SceneEntityAppearNotify
        {
            EntityList = { avatarEntity },
            AppearType = VisionType.Born,
        });

        return ValueTask.FromResult(Response(CmdType.EnterSceneDoneRsp, new EnterSceneDoneRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }

    [NetCommand(CmdType.SceneInitFinishReq)]
    public ValueTask<IResult> OnSceneInitFinishReq(Player player, BinDataCollection binData)
    {
        bool furinaExists = player.TryGetAvatar(10000089, out GameAvatar? gameAvatar); // Currently hardcode to 10000089. It's definitely exists because of UnlockAllAvatars()
        if (!furinaExists) throw new InvalidOperationException("Furina doesn't exist? It's FurinaImpact, you should have Furina.");

        AddNotify(CmdType.PlayerEnterSceneInfoNotify, new PlayerEnterSceneInfoNotify
        {
            CurAvatarEntityId = AvatarEntityId,
            EnterSceneToken = EnterSceneToken,
            MpLevelEntityInfo = new MPLevelEntityInfo
            {
                EntityId = 184549377,
                AbilityInfo = new AbilitySyncStateInfo
                {
                    IsInited = false,
                },
                AuthorityPeerId = 1
            },
            AvatarEnterInfo =
            {
                new AvatarEnterSceneInfo
                {
                    WeaponGuid = GameAvatar.WeaponGuid,
                    AvatarEntityId = AvatarEntityId,
                    WeaponEntityId = WeaponEntityId,
                    AvatarGuid = gameAvatar!.Guid,
                }
            },
            TeamEnterInfo = new TeamEnterSceneInfo
            {
                TeamEntityId = 150994946,
                AbilityControlBlock = new AbilityControlBlock(),
                TeamAbilityInfo = new AbilitySyncStateInfo()
            }
        });

        SceneTeamAvatar sceneTeamAvatar = new()
        {
            SceneEntityInfo = new SceneEntityInfo
            {
                EntityType = ProtEntityType.Avatar,
                EntityId = AvatarEntityId,
                MotionInfo = new MotionInfo
                {
                    Pos = new Vector
                    {
                        X = 2336.789f,
                        Y = 249.98996f,
                        Z = -751.3081f
                    },
                    Rot = new Vector(),
                    Speed = new Vector(),
                    State = MotionState.None,
                },
                LifeState = 1, // ALIVE
                AnimatorParaList = { new AnimatorParameterValueInfoPair() },
                Avatar = new SceneAvatarInfo
                {
                    Uid = player.Uid,
                    AvatarId = gameAvatar.AvatarId,
                    Guid = gameAvatar.Guid,
                    PeerId = 1,
                    EquipIdList = { gameAvatar.WeaponId },
                    SkillDepotId = gameAvatar.SkillDepotId,
                    Weapon = new SceneWeaponInfo
                    {
                        EntityId = WeaponEntityId,
                        GadgetId = 50000000 + gameAvatar.WeaponId,
                        ItemId = gameAvatar.WeaponId,
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
                    WearingFlycloakId = gameAvatar.WearingFlycloakId,
                    BornTime = gameAvatar.BornTime,
                    CostumeId = 0,
                    AnimHash = 0
                },
                LastMoveSceneTimeMs = 0,
                LastMoveReliableSeq = 0,
                EntityClientData = new(),
                EntityAuthorityInfo = new EntityAuthorityInfo
                {
                    AbilityInfo = new(),
                    RendererChangedInfo = new(),
                    AiInfo = new()
                    {
                        IsAiOpen = true,
                        BornPos = new()
                    },
                    BornPos = new(),
                },
            },
            WeaponEntityId = WeaponEntityId,
            PlayerUid = player.Uid,
            WeaponGuid = GameAvatar.WeaponGuid,
            EntityId = AvatarEntityId,
            AvatarGuid = gameAvatar.Guid,
            AbilityControlBlock = new(),
            SceneId = 3,
        };

        foreach (PropValue propValue in gameAvatar.Properties)
        {
            sceneTeamAvatar.SceneEntityInfo.PropList.Add(new PropPair { Type = propValue.Type, PropValue = propValue });
        }

        foreach (FightPropPair pair in gameAvatar.FightProperties)
        {
            sceneTeamAvatar.SceneEntityInfo.FightPropList.Add(pair);
        }

        AvatarConfig avatarConfig = binData.GetAvatarConfig(gameAvatar.AvatarId);

        uint defaultOverrideHash = "Default".GetStableHash();
        foreach (string abilityName in binData.CommonAbilities)
        {
            sceneTeamAvatar.AbilityControlBlock.AbilityEmbryoList.Add(new AbilityEmbryo
            {
                AbilityId = (uint)(sceneTeamAvatar.AbilityControlBlock.AbilityEmbryoList.Count + 1),
                AbilityNameHash = abilityName.GetStableHash(),
                AbilityOverrideNameHash = defaultOverrideHash
            });
        }

        foreach (AbilityData ability in avatarConfig.Abilities)
        {
            sceneTeamAvatar.AbilityControlBlock.AbilityEmbryoList.Add(new AbilityEmbryo
            {
                AbilityId = (uint)(sceneTeamAvatar.AbilityControlBlock.AbilityEmbryoList.Count + 1),
                AbilityNameHash = ability.AbilityName.GetStableHash(),
                AbilityOverrideNameHash = ability.GetAbilityOverride().GetStableHash()
            });
        }

        AddNotify(CmdType.SceneTeamUpdateNotify, new SceneTeamUpdateNotify
        {
            SceneTeamAvatarList = { sceneTeamAvatar }
        });

        return ValueTask.FromResult(Response(CmdType.SceneInitFinishRsp, new SceneInitFinishRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }

    [NetCommand(CmdType.EnterSceneReadyReq)]
    public ValueTask<IResult> OnEnterSceneReadyReq()
    {
        AddNotify(CmdType.EnterScenePeerNotify, new EnterScenePeerNotify
        {
            DestSceneId = 3,
            EnterSceneToken = EnterSceneToken,
            HostPeerId = 1,
            PeerId = 1
        });

        return ValueTask.FromResult(Response(CmdType.EnterSceneReadyRsp, new EnterSceneReadyRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }
}
