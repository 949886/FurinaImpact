using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Game.Scene;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class SceneController : ControllerBase
{
    // TODO: Scene management, Entity management!!!
    public const uint WeaponEntityId = 100663300;

    [NetCommand(CmdType.PostEnterSceneReq)]
    public async ValueTask<IResult> OnPostEnterSceneReq(SceneManager sceneManager)
    {
        await sceneManager.OnEnterStateChanged(SceneEnterState.PostEnter);

        return Response(CmdType.PostEnterSceneRsp, new PostEnterSceneRsp
        {
            EnterSceneToken = sceneManager.EnterToken
        });
    }

    [NetCommand(CmdType.EnterSceneDoneReq)]
    public async ValueTask<IResult> OnEnterSceneDoneReq(SceneManager sceneManager)
    {
        await sceneManager.OnEnterStateChanged(SceneEnterState.EnterDone);

        return Response(CmdType.EnterSceneDoneRsp, new EnterSceneDoneRsp
        {
            EnterSceneToken = sceneManager.EnterToken
        });
    }

    [NetCommand(CmdType.SceneInitFinishReq)]
    public async ValueTask<IResult> OnSceneInitFinishReq(SceneManager sceneManager)
    {
        await sceneManager.OnEnterStateChanged(SceneEnterState.InitFinished);

        return Response(CmdType.SceneInitFinishRsp, new SceneInitFinishRsp
        {
            EnterSceneToken = sceneManager.EnterToken
        });
    }

    [NetCommand(CmdType.EnterSceneReadyReq)]
    public async ValueTask<IResult> OnEnterSceneReadyReq(SceneManager sceneManager)
    {
        await sceneManager.OnEnterStateChanged(SceneEnterState.ReadyToEnter);

        return Response(CmdType.EnterSceneReadyRsp, new EnterSceneReadyRsp
        {
            EnterSceneToken = sceneManager.EnterToken
        });
    }
}
