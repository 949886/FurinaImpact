using FurinaImpact.Gameserver.Game.Avatar;

namespace FurinaImpact.Gameserver.Game.Entity.Factory;
internal class EntityFactory
{
    private uint _entityIdSeed;

    public AvatarEntity CreateAvatar(GameAvatar gameAvatar, uint belongUid)
    {
        return new(gameAvatar, belongUid, ++_entityIdSeed);
    }
}
