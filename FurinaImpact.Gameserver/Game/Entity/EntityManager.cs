using FurinaImpact.Gameserver.Game.Entity.Listener;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Entity;
internal class EntityManager
{
    private readonly List<SceneEntity> _entities;
    private readonly IEntityEventListener _listener;

    public EntityManager(IEntityEventListener listener)
    {
        _entities = new List<SceneEntity>();
        _listener = listener;
    }

    public async ValueTask SpawnEntityAsync(SceneEntity entity, VisionType visionType)
    {
        _entities.Add(entity);
        await _listener.OnEntitySpawned(entity, visionType);
    }

    public void Reset()
    {
        _entities.Clear();
    }
}
