using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Game.Scene;
using FurinaImpact.Gameserver.Network.Session;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers;

[NetController]
internal class AvatarController : ControllerBase
{
    [NetCommand(CmdType.SetUpAvatarTeamReq)]
    public async ValueTask<IResult> OnSetUpAvatarTeamReq(NetSession session, SceneManager sceneManager)
    {
        SetUpAvatarTeamReq request = Packet!.DecodeBody<SetUpAvatarTeamReq>();

        AvatarTeam newTeam = new();
        newTeam.AvatarGuidList.AddRange(request.AvatarTeamGuidList);
        await session.NotifyAsync(CmdType.AvatarTeamUpdateNotify, new AvatarTeamUpdateNotify
        {
            AvatarTeamMap = { { request.TeamId, newTeam } }
        });

        await sceneManager.ChangeTeamAvatarsAsync(request.AvatarTeamGuidList.ToArray());

        SetUpAvatarTeamRsp response = new()
        {
            CurAvatarGuid = request.CurAvatarGuid,
            TeamId = request.TeamId,
        };
        response.AvatarTeamGuidList.AddRange(request.AvatarTeamGuidList);

        return Response(CmdType.SetUpAvatarTeamRsp, response);
    }
}
