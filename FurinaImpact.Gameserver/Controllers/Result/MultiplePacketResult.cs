using System.Diagnostics.CodeAnalysis;
using FurinaImpact.Gameserver.Network;

namespace FurinaImpact.Gameserver.Controllers.Result;
internal class MultiplePacketResult : IResult
{
    private readonly Queue<NetPacket> _sendQueue;

    public MultiplePacketResult()
    {
        _sendQueue = new Queue<NetPacket>();
    }

    public void Enqueue(NetPacket packet)
    {
        _sendQueue.Enqueue(packet);
    }

    public bool NextPacket([MaybeNullWhen(false)] out NetPacket packet)
    {
        return _sendQueue.TryDequeue(out packet);
    }
}
