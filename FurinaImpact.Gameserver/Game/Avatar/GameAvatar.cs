using System.Text.Json;
using FurinaImpact.Common.Constants;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Game.Avatar;
internal class GameAvatar
{
    public const ulong WeaponGuid = 2281337;

    public ulong Guid { get; set; }

    public uint AvatarId { get; set; }
    public uint SkillDepotId { get; set; }
    public uint WearingFlycloakId { get; set; }
    public uint BornTime { get; set; }

    public uint WeaponId { get; set; } // TODO: Weapon class!

    // Properties
    public uint Exp { get; set; }
    public uint BreakLevel { get; set; }
    public uint SmallTalentPoint { get; set; }
    public uint BigTalentPoint { get; set; }
    public uint GearStartVal { get; set; }
    public uint GearStopVal { get; set; }
    public uint Level { get; set; }

    public AvatarInfo AsAvatarInfo()
    {
        AvatarInfo info = new()
        {
            Guid = Guid,
            AvatarId = AvatarId,
            SkillDepotId = SkillDepotId,
            LifeState = 1,
            AvatarType = 1,
            WearingFlycloakId = WearingFlycloakId,
            BornTime = BornTime,
            FetterInfo = new() { ExpLevel = 1 },
            EquipGuidList = { WeaponGuid } // no weapon classes for now
        };

        info.PropMap.Add(PlayerProp.PROP_LEVEL, new() { Type = PlayerProp.PROP_LEVEL, Ival = Level });
        info.PropMap.Add(PlayerProp.PROP_EXP, new() { Type = PlayerProp.PROP_EXP, Ival = Exp });
        info.PropMap.Add(PlayerProp.PROP_BREAK_LEVEL, new() { Type = PlayerProp.PROP_BREAK_LEVEL, Ival = BreakLevel });
        info.PropMap.Add(PlayerProp.PROP_SATIATION_VAL, new() { Type = PlayerProp.PROP_SATIATION_VAL, Ival = 0 });
        info.PropMap.Add(PlayerProp.PROP_SATIATION_PENALTY_TIME, new() { Type = PlayerProp.PROP_SATIATION_PENALTY_TIME, Ival = 0 });

        // Currently hardcoded values
        Dictionary<uint, FightPropPair> fightProps = CreateFightProps();
        foreach (FightPropPair pair in fightProps.Values)
        {
            info.FightPropMap.Add(pair.PropType, pair.PropValue);
        }

        return info;
    }

    public static Dictionary<uint, FightPropPair> CreateFightProps() => new()
    {
        { FightProp.FIGHT_PROP_CUR_FIRE_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_FIRE_ENERGY, PropValue = 100 } },
        { FightProp.FIGHT_PROP_CUR_ELEC_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_ELEC_ENERGY, PropValue =  100 } },
        { FightProp.FIGHT_PROP_CUR_WATER_ENERGY,  new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_WATER_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_CUR_GRASS_ENERGY,  new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_GRASS_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_CUR_WIND_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_WIND_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_CUR_ICE_ENERGY,    new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_ICE_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_CUR_ROCK_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_ROCK_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_FIRE_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_FIRE_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_ELEC_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_ELEC_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_WATER_ENERGY,  new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_WATER_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_GRASS_ENERGY,  new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_GRASS_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_WIND_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_WIND_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_ICE_ENERGY,    new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_ICE_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_MAX_ROCK_ENERGY,   new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_ROCK_ENERGY, PropValue =  100 }},
        { FightProp.FIGHT_PROP_BASE_HP,           new FightPropPair { PropType = FightProp.FIGHT_PROP_BASE_HP, PropValue =  1000 } },
        { FightProp.FIGHT_PROP_BASE_ATTACK,       new FightPropPair { PropType = FightProp.FIGHT_PROP_BASE_ATTACK, PropValue =  100 } },
        { FightProp.FIGHT_PROP_BASE_DEFENSE,      new FightPropPair { PropType = FightProp.FIGHT_PROP_BASE_DEFENSE, PropValue =  50 } },
        { FightProp.FIGHT_PROP_CUR_HP,            new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_HP, PropValue =  1000 } },
        { FightProp.FIGHT_PROP_MAX_HP,            new FightPropPair { PropType = FightProp.FIGHT_PROP_MAX_HP, PropValue =  1000 } },
        { FightProp.FIGHT_PROP_CUR_ATTACK,        new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_ATTACK, PropValue =  100 } },
        { FightProp.FIGHT_PROP_CUR_DEFENSE,       new FightPropPair { PropType = FightProp.FIGHT_PROP_CUR_DEFENSE, PropValue =  50 } },
        { FightProp.FIGHT_PROP_CHARGE_EFFICIENCY, new FightPropPair { PropType = FightProp.FIGHT_PROP_CHARGE_EFFICIENCY, PropValue =   1 } },
        { FightProp.FIGHT_PROP_CRITICAL_HURT,     new FightPropPair { PropType = FightProp.FIGHT_PROP_CRITICAL_HURT, PropValue =  0.5f } },
        { FightProp.FIGHT_PROP_CRITICAL,          new FightPropPair { PropType = FightProp.FIGHT_PROP_CRITICAL, PropValue =  0.05f } }
    };
}
