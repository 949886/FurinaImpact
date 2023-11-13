using FurinaImpact.Gameserver.Game.Entity;
using FurinaImpact.Gameserver.Game.Entity.Listener;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Network.Session;
internal class SessionEntityEventListener : IEntityEventListener
{
    private readonly NetSession _session;

    public SessionEntityEventListener(NetSession session)
    {
        _session = session;
    }

    public async ValueTask OnEntitySpawned(SceneEntity entity, VisionType visionType)
    {
        await _session.NotifyAsync(CmdType.SceneEntityAppearNotify, new SceneEntityAppearNotify
        {
            AppearType = visionType,
            EntityList = { entity.AsInfo() }
        });
    }
}
