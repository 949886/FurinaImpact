namespace FurinaImpact.Gameserver.Game.Avatar;
internal class GameAvatarTeam
{
    public uint Index { get; set; }
    public List<ulong> AvatarGuidList { get; set; }

    public GameAvatarTeam()
    {
        AvatarGuidList = new();
    }
}
