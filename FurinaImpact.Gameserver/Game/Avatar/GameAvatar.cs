using FurinaImpact.Common.Constants;
using FurinaImpact.Common.Data.Excel;
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
    public List<PropValue> Properties;
    public List<FightPropPair> FightProperties;

    public GameAvatar()
    {
        Properties = new List<PropValue>();
        FightProperties = new List<FightPropPair>();
    }

    public void InitDefaultProps(AvatarExcel avatarExcel)
    {
        Properties.Clear();
        FightProperties.Clear();

        SetProp(PlayerProp.PROP_LEVEL, 1);
        SetProp(PlayerProp.PROP_EXP, 0);
        SetProp(PlayerProp.PROP_BREAK_LEVEL, 0);

        float baseHp = (float)avatarExcel.HpBase;
        float baseAttack = (float)avatarExcel.AttackBase;
        float baseDefense = (float)avatarExcel.DefenseBase;

        SetFightProp(FightProp.FIGHT_PROP_BASE_HP, baseHp);
        SetFightProp(FightProp.FIGHT_PROP_CUR_HP, baseHp);
        SetFightProp(FightProp.FIGHT_PROP_MAX_HP, baseHp);

        SetFightProp(FightProp.FIGHT_PROP_BASE_ATTACK, baseAttack);
        SetFightProp(FightProp.FIGHT_PROP_CUR_ATTACK, baseAttack);

        SetFightProp(FightProp.FIGHT_PROP_BASE_DEFENSE, baseDefense);
        SetFightProp(FightProp.FIGHT_PROP_CUR_DEFENSE, baseDefense);

        SetFightProp(FightProp.FIGHT_PROP_CHARGE_EFFICIENCY, (float)avatarExcel.ChargeEfficiency);
        SetFightProp(FightProp.FIGHT_PROP_CRITICAL_HURT, (float)avatarExcel.CriticalHurt);
        SetFightProp(FightProp.FIGHT_PROP_CRITICAL, (float)avatarExcel.Critical);

        SetFightProp(FightProp.FIGHT_PROP_CUR_FIRE_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_ELEC_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_WATER_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_GRASS_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_WIND_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_ICE_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_CUR_ROCK_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_FIRE_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_ELEC_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_WATER_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_GRASS_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_WIND_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_ICE_ENERGY, 100);
        SetFightProp(FightProp.FIGHT_PROP_MAX_ROCK_ENERGY, 100);
    }

    public void SetProp(uint propType, uint value)
    {
        PropValue? prop = Properties.Find(p => p.Type == propType);
        if (prop != null)
        {
            prop.Ival = value;
            return;
        }

        Properties.Add(new PropValue { Type = propType, Ival = value });
    }

    public void SetProp(uint propType, float value)
    {
        PropValue? prop = Properties.Find(p => p.Type == propType);
        if (prop != null)
        {
            prop.Fval = value;
            return;
        }

        Properties.Add(new PropValue { Type = propType, Fval = value });
    }

    public void SetFightProp(uint propType, float value)
    {
        FightPropPair? fightPropPair = FightProperties.Find(pair => pair.PropType == propType);
        if (fightPropPair != null)
        {
            fightPropPair.PropValue = value;
            return;
        }

        FightProperties.Add(new FightPropPair { PropType = propType, PropValue = value });
    }

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

        foreach (PropValue prop in Properties)
        {
            info.PropMap.Add(prop.Type, prop);
        }

        foreach (FightPropPair pair in FightProperties)
        {
            info.FightPropMap.Add(pair.PropType, pair.PropValue);
        }

        return info;
    }
}
