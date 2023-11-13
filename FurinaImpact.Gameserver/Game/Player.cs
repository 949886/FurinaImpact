using System.Diagnostics.CodeAnalysis;
using FurinaImpact.Common.Data.Excel;
using FurinaImpact.Gameserver.Game.Avatar;

namespace FurinaImpact.Gameserver.Game;
internal class Player
{
    private static readonly uint[] AvatarBlackList = { 10000001, 10000005, 10000007 }; // kate and travelers

    public uint Uid { get; set; }
    public uint GuidSeed { get; set; }
    public string Name { get; set; }
    public List<GameAvatar> Avatars { get; set; }

    private readonly ExcelTableCollection _excelTables;

    public Player(ExcelTableCollection excelTables)
    {
        Name = "Traveler";
        Avatars = new();

        _excelTables = excelTables;
    }

    public void InitDefaultPlayer()
    {
        // We don't have database atm, so let's init default player state for every session.

        Uid = 1337;
        Name = "FurinaImpact";

        UnlockAllAvatars();
    }

    public bool TryGetAvatar(uint avatarId, [MaybeNullWhen(false)] out GameAvatar avatar)
        => (avatar = Avatars.Find(a => a.AvatarId == avatarId)) != null;

    private void UnlockAllAvatars()
    {
        ExcelTable avatarTable = _excelTables.GetTable(ExcelType.Avatar);
        for (int i = 0; i < avatarTable.Count; i++)
        {
            AvatarExcel avatarExcel = avatarTable.GetItemAt<AvatarExcel>(i);
            if (AvatarBlackList.Contains(avatarExcel.Id) || avatarExcel.Id >= 11000000) continue;

            uint currentTimestamp = (uint)DateTimeOffset.Now.ToUnixTimeSeconds();
            Avatars.Add(new()
            {
                AvatarId = avatarExcel.Id,
                SkillDepotId = avatarExcel.SkillDepotId,
                WeaponId = avatarExcel.InitialWeapon,
                BornTime = currentTimestamp,
                Level = 1,
                Guid = NextGuid(),
                WearingFlycloakId = 140001
            });
        }
    }

    public ulong NextGuid()
    {
        return ((ulong)Uid << 32) + (++GuidSeed);
    }
}
