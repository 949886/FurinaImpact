using System.Diagnostics.CodeAnalysis;
using FurinaImpact.Gameserver.Network;

namespace FurinaImpact.Gameserver.Controllers.Result;
internal class SinglePacketResult : IResult
{
    private NetPacket? _packet;

    public SinglePacketResult(NetPacket? packet)
    {
        _packet = packet;
    }

    public bool NextPacket([MaybeNullWhen(false)] out NetPacket packet)
    {
        packet = _packet;
        _packet = null;

        return packet != null;
    }
}
