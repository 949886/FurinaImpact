using FurinaImpact.Common.Constants;
using FurinaImpact.Common.Security;
using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class AccountController : ControllerBase
{
    [NetCommand(CmdType.GetPlayerTokenReq)]
    public ValueTask<IResult> OnGetPlayerTokenReq()
    {
        return ValueTask.FromResult(Response(CmdType.GetPlayerTokenRsp, new GetPlayerTokenRsp
        {
            ServerRandKey = Convert.ToBase64String(MhySecurity.EncryptWithRSA(new byte[8])),
            Sign = string.Empty, // bypassed
            Uid = 1337,
            CountryCode = "RU",
            PlatformType = 3
        }));
    }

    [NetCommand(CmdType.PingReq)]
    public ValueTask<IResult> OnPingReq()
    {
        return ValueTask.FromResult(Response(CmdType.PingRsp, new PingRsp
        {
            ServerTime = (uint)DateTimeOffset.Now.ToUnixTimeSeconds()
        }));
    }

    [NetCommand(CmdType.PlayerLoginReq)]
    public ValueTask<IResult> OnPlayerLoginReq()
    {
        AddNotify(CmdType.PlayerDataNotify, new PlayerDataNotify
        {
            NickName = "FurinaImpact",
            PropMap =
            {
                {PlayerProp.PROP_PLAYER_LEVEL, new() { Type = PlayerProp.PROP_PLAYER_LEVEL, Ival = 5 } },
                {PlayerProp.PROP_IS_FLYABLE, new() { Type = PlayerProp.PROP_IS_FLYABLE, Ival = 1 } },
                {PlayerProp.PROP_MAX_STAMINA, new() { Type = PlayerProp.PROP_MAX_STAMINA, Ival = 10000 } },
                {PlayerProp.PROP_CUR_PERSIST_STAMINA, new() { Type = PlayerProp.PROP_CUR_PERSIST_STAMINA, Ival = 10000 } },
                {PlayerProp.PROP_IS_TRANSFERABLE, new() { Type = PlayerProp.PROP_IS_TRANSFERABLE, Ival = 1 } },
                {PlayerProp.PROP_IS_SPRING_AUTO_USE, new() { Type = PlayerProp.PROP_IS_SPRING_AUTO_USE, Ival = 1 } },
                {PlayerProp.PROP_SPRING_AUTO_USE_PERCENT, new() { Type = PlayerProp.PROP_SPRING_AUTO_USE_PERCENT, Ival = 50 } }
            }
        });

        AddNotify(CmdType.AvatarDataNotify, new AvatarDataNotify
        {
            AvatarList =
            {
                new AvatarInfo
                {
                    AvatarId = 10000089,
                    Guid = 5742371274756L,
                    PropMap =
                    {
                        { 4001, new PropValue
                        {
                            Type = 4001,
                            Ival = 1L,
                            Fval = 0f,
                            Val = 1L
                        } },
                        { 1001, new PropValue
                        {
                            Type = 1001,
                            Ival = 0L,
                            Fval = 0f,
                            Val = 0L
                        } },
                        { 1002, new PropValue
                        {
                            Type = 1002,
                            Ival = 0L,
                            Fval = 0f,
                            Val = 0L
                        } },
                        { 1003, new PropValue
                        {
                            Type = 1003,
                            Ival = 0L,
                            Fval = 0f,
                            Val = 0L
                        } },
                        { 1004, new PropValue
                        {
                            Type = 1004,
                            Ival = 0L,
                            Fval = 0f,
                            Val = 0L
                        } }
                    },
                    LifeState = 1,
                    EquipGuidList =
                    {
                        5742371274781L
                    },
                    FightPropMap =
                    {
                        { 1004, 0f },
                        { 1010, 1039.4418f },
                        { 4, 42.655724f },
                        { 2001, 42.655724f },
                        { 2002, 59.685677f },
                        { 2000, 1039.4418f },
                        { 74, 70f },
                        { 1, 1039.4418f },
                        { 7, 59.685677f },
                        { 23, 1f },
                        { 22, 0.5f },
                        { 20, 0.05f }
                    },
                    SkillDepotId = 8901,
                    FetterInfo = new AvatarFetterInfo
                    {
                        ExpNumber = 0,
                        ExpLevel = 1,
                        OpenIdList = {},
                        FinishIdList = {},
                        RewardedFetterLevelList = {},
                        FetterList = { }
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
                    ExpeditionState = AvatarExpeditionState.None,
                    ProudSkillExtraLevelMap =
                    {
                        { 8331, 0 },
                        { 8332, 0 },
                        { 8339, 0 }
                    },
                    AvatarType = 1,
                    WearingFlycloakId = 140001,
                    BornTime = 1692707580,
                },
            },
            CurAvatarTeamId = 1,
            ChooseAvatarGuid = 5742371274753L,
            AvatarTeamMap =
            {
                { 1, new AvatarTeam
                {
                    AvatarGuidList =
                    {
                        5742371274756L,
                    }
                } },
            },
        });

        AddNotify(CmdType.OpenStateUpdateNotify, new OpenStateUpdateNotify
        {
            OpenStateMap =
            {
                {1, 1},
                {2, 1},
                {3, 1},
                {4, 1},
                {5, 1},
                {6, 1},
                {7, 0},
                {8, 1},
                {10, 1},
                {11, 1},
                {12, 1},
                {13, 1},
                {14, 1},
                {15, 1},
                {27, 1},
                {28, 1},
                {29, 1},
                {30, 1},
                {31, 1},
                {32, 1},
                {33, 1},
                {37, 1},
                {38, 1},
                {45, 1},
                {47, 1},
                {53, 1},
                {54, 1},
                {55, 1},
                {59, 1},
                {62, 1},
                {65, 1},
                {900, 1},
                {901, 1},
                {902, 1},
                {903, 1},
                {1001, 1},
                {1002, 1},
                {1003, 1},
                {1004, 1},
                {1005, 1},
                {1007, 1},
                {1008, 1},
                {1009, 1},
                {1010, 1},
                {1100, 1},
                {1103, 1},
                {1300, 1},
                {1401, 1},
                {1403, 1},
                {1700, 1},
                {2100, 1},
                {2101, 1},
                {2103, 1},
                {2400, 1},
                {3701, 1},
                {3702, 1},
                {4100, 1 } 
            }
        });

        AddNotify(CmdType.PlayerEnterSceneNotify, new PlayerEnterSceneNotify
        {
            SceneTagIdList = { },
            SceneBeginTime = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds(),
            SceneId = 3,
            SceneTransaction = "3-1337-1699517719-13830",
            Pos = new()
            {
                X = 2191.16357421875f,
                Y = 214.65115356445312f,
                Z = -1120.633056640625f
            },
            TargetUid = 1337,
            UnkUid1020 = 1337,
            EnterSceneToken = SceneController.EnterSceneToken,
            PrevPos = new(),
            Unk13 = 1,
            Unk3 = 1,
            Unk449 = 1,
            Unk834 = 1
        });

        return ValueTask.FromResult(Response(CmdType.PlayerLoginRsp, new PlayerLoginRsp
        {
            CountryCode = "RU",
            GameBiz = "hk4e_global",
            ResVersionConfig = new()
        }));
    }
}
