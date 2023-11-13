using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Entity.Listener;
internal interface IEntityEventListener
{
    ValueTask OnEntitySpawned(SceneEntity entity, VisionType visionType);
}
