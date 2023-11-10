namespace FurinaImpact.Protocol;
public enum CmdType
{
    GetPlayerTokenReq = 28214,
    GetPlayerTokenRsp = 1574,

    PingReq = 6603,
    PingRsp = 2794,

    // Step 1
    PlayerLoginReq = 24761,
    PlayerDataNotify = 1442,
    AvatarDataNotify = 71,
    OpenStateUpdateNotify = 7518,
    PlayerEnterSceneNotify = 5390,
    PlayerLoginRsp = 1548,

    // Step 2
    EnterSceneReadyReq = 27445,
    EnterScenePeerNotify = 20740,
    EnterSceneReadyRsp = 25384,

    // Step 3
    SceneInitFinishReq = 4530,
    PlayerEnterSceneInfoNotify = 26842,
    SceneTeamUpdateNotify = 25695,
    SceneInitFinishRsp = 26180,

    // Step 4
    EnterSceneDoneReq = 29246,
    SceneEntityAppearNotify = 1050,
    EnterSceneDoneRsp = 9339,

    // Step 5
    PostEnterSceneReq = 26222,
    PostEnterSceneRsp = 23949
}