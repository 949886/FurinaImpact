using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class SceneController : ControllerBase
{
    public const uint EnterSceneToken = 19483;

    [NetCommand(CmdType.PostEnterSceneReq)]
    public ValueTask<IResult> OnPostEnterSceneReq()
    {
        return ValueTask.FromResult(Response(CmdType.PostEnterSceneRsp, new PostEnterSceneRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }

    [NetCommand(CmdType.EnterSceneDoneReq)]
    public ValueTask<IResult> OnEnterSceneDoneReq()
    {
        AddNotify(CmdType.SceneEntityAppearNotify, new SceneEntityAppearNotify
        {
            EntityList =
            {
                new SceneEntityInfo
                {
                    EntityType = ProtEntityType.Avatar,
                    EntityId = 16777219,
                    MotionInfo = new MotionInfo
                    {
                        Pos = new Vector
                        {
                            X = 2336.788f,
                            Y = 249.98996f,
                            Z = -751.3081f
                        },
                        Rot = new Vector
                        {
                            X = 0f,
                            Y = 117.238686f,
                            Z = 0f
                        },
                        Speed = new Vector(),
                        State = MotionState.None,
                    },
                    PropList =
                    {
                        new PropPair
                        {
                            Type = 4001,
                            PropValue = new PropValue
                            {
                                Type = 4001,
                                Ival = 1L,
                                Fval = 0f,
                                Val = 1L
                            }
                        }
                    },
                    FightPropList =
                    {
                        new FightPropPair
                        {
                            PropType = 1004,
                            PropValue = 0f
                        },
                        new FightPropPair
                        {
                            PropType = 1010,
                            PropValue = 1039.4418f
                        },
                        new FightPropPair
                        {
                            PropType = 4,
                            PropValue = 42.655724f
                        },
                        new FightPropPair
                        {
                            PropType = 2001,
                            PropValue = 42.655724f
                        },
                        new FightPropPair
                        {
                            PropType = 2002,
                            PropValue = 59.685677f
                        },
                        new FightPropPair
                        {
                            PropType = 2000,
                            PropValue = 1039.4418f
                        },
                        new FightPropPair
                        {
                            PropType = 74,
                            PropValue = 70f
                        },
                        new FightPropPair
                        {
                            PropType = 1,
                            PropValue = 1039.4418f
                        },
                        new FightPropPair
                        {
                            PropType = 7,
                            PropValue = 59.685677f
                        },
                        new FightPropPair
                        {
                            PropType = 23,
                            PropValue = 1f
                        },
                        new FightPropPair
                        {
                            PropType = 22,
                            PropValue = 0.5f
                        },
                        new FightPropPair
                        {
                            PropType = 20,
                            PropValue = 0.05f
                        }
                    },
                    LifeState = 1,
                    AnimatorParaList =
                    {
                        new AnimatorParameterValueInfoPair()
                    },
                    Avatar = new SceneAvatarInfo
                    {
                        Uid = 1337,
                        AvatarId = 10000089,
                        Guid = 5742371274756L,
                        PeerId = 1,
                        EquipIdList =
                        {
                            11101
                        },
                        SkillDepotId = 8901,
                        Weapon = new SceneWeaponInfo
                        {
                            EntityId = 100663300,
                            GadgetId = 50011101,
                            ItemId = 11101,
                            Guid = 5742371274781L,
                            Level = 1,
                            PromoteLevel = 0,
                            AbilityInfo = new()
                        },
                        ReliquaryList = { },
                        CoreProudSkillLevel = 0,
                        InherentProudSkillList =
                        {
                            832301
                        },
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
                        TeamResonanceList =
                        {
                            10301
                        },
                        WearingFlycloakId = 140001,
                        BornTime = 1692707580,
                        CostumeId = 0,
                        AnimHash = 0
                    },
                    LastMoveSceneTimeMs = 0,
                    LastMoveReliableSeq = 0,
                    EntityClientData = new EntityClientData
                    {
                        WindChangeSceneTime = 0,
                        WindmillSyncAngle = 0f,
                        WindChangeTargetLevel = 0
                    },
                    EntityAuthorityInfo = new EntityAuthorityInfo
                    {
                        AbilityInfo = new AbilitySyncStateInfo
                        {
                            IsInited = false,

                        },
                        RendererChangedInfo = new EntityRendererChangedInfo
                        {
                            // ChangedRenderers = null,
                            VisibilityCount = 0,
                            IsCached = false
                        },
                        AiInfo = new SceneEntityAiInfo
                        {
                            IsAiOpen = true,
                            BornPos = new Vector
                            {
                                X = 0f,
                                Y = 0f,
                                Z = 0f
                            },
                            CurTactic = 0,
                        },
                        BornPos = new()
                    },
                }
            },
            AppearType = VisionType.Born,
        });

        return ValueTask.FromResult(Response(CmdType.EnterSceneDoneRsp, new EnterSceneDoneRsp
        {
            EnterSceneToken = EnterSceneToken
        }));
    }

    [NetCommand(CmdType.SceneInitFinishReq)]
    public ValueTask<IResult> OnSceneInitFinishReq()
    {
        AddNotify(CmdType.PlayerEnterSceneInfoNotify, new PlayerEnterSceneInfoNotify
        {
            CurAvatarEntityId = 16777219,
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
                    WeaponGuid = 5742371274781L,
                    AvatarEntityId = 16777219,
                    WeaponEntityId = 100663300,
                    AvatarGuid = 5742371274756L,
                }
            },
            TeamEnterInfo = new TeamEnterSceneInfo
            {
                TeamEntityId = 150994946,
                AbilityControlBlock = new AbilityControlBlock(),
                TeamAbilityInfo = new AbilitySyncStateInfo()
            }
        });

        AddNotify(CmdType.SceneTeamUpdateNotify, new SceneTeamUpdateNotify
        {
            SceneTeamAvatarList =
            {
                new SceneTeamAvatar
                {
                    SceneEntityInfo = new SceneEntityInfo
                    {
                        EntityType = ProtEntityType.Avatar,
                        EntityId = 16777219,
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
                        PropList =
                        {
                            new PropPair
                            {
                                Type = 4001,
                                PropValue = new PropValue
                                {
                                    Type = 4001,
                                    Ival = 1L,
                                    Fval = 0f,
                                    Val = 1L
                                }
                            }
                        },
                        FightPropList =
                        {
                            new FightPropPair
                            {
                                PropType = 1004,
                                PropValue = 0f
                            },
                            new FightPropPair
                            {
                                PropType = 1010,
                                PropValue = 1039.4418f
                            },
                            new FightPropPair
                            {
                                PropType = 4,
                                PropValue = 42.655724f
                            },
                            new FightPropPair
                            {
                                PropType = 2001,
                                PropValue = 42.655724f
                            },
                            new FightPropPair
                            {
                                PropType = 2002,
                                PropValue = 59.685677f
                            },
                            new FightPropPair
                            {
                                PropType = 2000,
                                PropValue = 1039.4418f
                            },
                            new FightPropPair
                            {
                                PropType = 74,
                                PropValue = 70f
                            },
                            new FightPropPair
                            {
                                PropType = 1,
                                PropValue = 1039.4418f
                            },
                            new FightPropPair
                            {
                                PropType = 7,
                                PropValue = 59.685677f
                            },
                            new FightPropPair
                            {
                                PropType = 23,
                                PropValue = 1f
                            },
                            new FightPropPair
                            {
                                PropType = 22,
                                PropValue = 0.5f
                            },
                            new FightPropPair
                            {
                                PropType = 20,
                                PropValue = 0.05f
                            }
                        },
                        LifeState = 1,
                        AnimatorParaList =
                        {
                            new AnimatorParameterValueInfoPair()
                        },
                        Avatar = new SceneAvatarInfo
                        {
                            Uid = 1337,
                            AvatarId = 10000089,
                            Guid = 5742371274756L,
                            PeerId = 1,
                            EquipIdList =
                            {
                                11101
                            },
                            SkillDepotId = 8901,
                            Weapon = new SceneWeaponInfo
                            {
                                EntityId = 100663300,
                                GadgetId = 50011101,
                                ItemId = 11101,
                                Guid = 5742371274781L,
                                Level = 1,
                                PromoteLevel = 0,
                                AbilityInfo = new AbilitySyncStateInfo
                                {
                                    IsInited = false,
                                }
                            },
                            CoreProudSkillLevel = 0,
                            InherentProudSkillList =
                            {
                                832301
                            },
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
                            TeamResonanceList =
                            {
                                10301
                            },
                            WearingFlycloakId = 140001,
                            BornTime = 1692707580,
                            CostumeId = 0,
                            AnimHash = 0
                        },
                        LastMoveSceneTimeMs = 0,
                        LastMoveReliableSeq = 0,
                        EntityClientData = new EntityClientData
                        {
                            WindChangeSceneTime = 0,
                            WindmillSyncAngle = 0f,
                            WindChangeTargetLevel = 0
                        },
                        EntityAuthorityInfo = new EntityAuthorityInfo
                        {
                            AbilityInfo = new AbilitySyncStateInfo
                            {
                                IsInited = false,

                            },
                            RendererChangedInfo = new EntityRendererChangedInfo
                            {
                                VisibilityCount = 0,
                                IsCached = false
                            },
                            AiInfo = new SceneEntityAiInfo
                            {
                                IsAiOpen = true,
                                BornPos = new(),
                            },
                            BornPos = new(),
                        },
                    },
                    WeaponEntityId = 100663300,
                    PlayerUid = 1337,
                    WeaponGuid = 5742371274781L,
                    EntityId = 16777219,
                    AvatarGuid = 5742371274756L,
                    AbilityControlBlock = new AbilityControlBlock
                    {
                        // Hardcoded Furina abilities!
                        AbilityEmbryoList =
                        {
                            new AbilityEmbryo
                            {
                                AbilityId = 4,
                                AbilityNameHash = 1771261036,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 5,
                                AbilityNameHash = 1579824719,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 6,
                                AbilityNameHash = 1761247227,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 7,
                                AbilityNameHash = 2724697652,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 8,
                                AbilityNameHash = 2717776381,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 9,
                                AbilityNameHash = 3823646769,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 10,
                                AbilityNameHash = 1972985736,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 11,
                                AbilityNameHash = 3187135836,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 12,
                                AbilityNameHash = 2678974399,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 13,
                                AbilityNameHash = 1771196189,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 14,
                                AbilityNameHash = 2306062007,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 15,
                                AbilityNameHash = 3105629177,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 16,
                                AbilityNameHash = 3771526669,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 17,
                                AbilityNameHash = 100636247,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 18,
                                AbilityNameHash = 1564404322,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 19,
                                AbilityNameHash = 497711942,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 20,
                                AbilityNameHash = 3531639848,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 21,
                                AbilityNameHash = 4255783285,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 22,
                                AbilityNameHash = 3829597473,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 23,
                                AbilityNameHash = 4183357155,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 24,
                                AbilityNameHash = 3052628990,
                                AbilityOverrideNameHash = 1178079449
                            },
                            new AbilityEmbryo
                            {
                                AbilityId = 25,
                                AbilityNameHash = 825255509,
                                AbilityOverrideNameHash = 1178079449
                            }
                        }
                    },
                    SceneId = 3,
                }
            }
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
