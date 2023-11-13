using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Entity;
internal abstract class SceneEntity
{
    public abstract ProtEntityType EntityType { get; }

    public uint EntityId { get; }
    public MotionInfo MotionInfo { get; set; }
    public List<PropValue> Properties { get; set; }
    public List<FightPropPair> FightProperties { get; set; }

    public SceneEntity(uint entityId)
    {
        EntityId = ((uint)EntityType << 24) + entityId;

        MotionInfo = new() { Pos = new(), Rot = new(), Speed = new() };
        Properties = new();
        FightProperties = new();
    }

    public void SetPosition(float x, float y, float z)
    {
        MotionInfo.Pos.X = x;
        MotionInfo.Pos.Y = y;
        MotionInfo.Pos.Z = z;
    }

    public void SetRotation(float x, float y, float z)
    {
        MotionInfo.Rot.X = x;
        MotionInfo.Rot.Y = y;
        MotionInfo.Rot.Z = z;
    }

    public virtual SceneEntityInfo AsInfo()
    {
        SceneEntityInfo info = new()
        {
            EntityType = EntityType,
            EntityId = EntityId,
            MotionInfo = MotionInfo,
            LifeState = 1,
            EntityClientData = new(),
            EntityAuthorityInfo = new EntityAuthorityInfo
            {
                AbilityInfo = new(),
                AiInfo = new()
                {
                    IsAiOpen = true,
                    BornPos = new()
                },
                BornPos = new(),
                ClientExtraInfo = new(),
                RendererChangedInfo = new()
            },
            AnimatorParaList = { new AnimatorParameterValueInfoPair() }
        };

        foreach (PropValue prop in Properties)
        {
            info.PropList.Add(new PropPair { Type = prop.Type, PropValue = prop });
        }

        info.FightPropList.AddRange(FightProperties);

        return info;
    }
}
