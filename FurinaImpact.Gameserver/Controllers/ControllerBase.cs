using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Network;
using FurinaImpact.Protocol;
using Google.Protobuf;

namespace FurinaImpact.Gameserver.Controllers;
internal abstract class ControllerBase
{
    public NetPacket? Packet { get; set; }
    private MultiplePacketResult? _currentResult;

    protected IResult Ok()
    {
        return _currentResult != null ? _currentResult : new SinglePacketResult(null);
    }

    protected void AddNotify<TMessage>(CmdType cmdType, TMessage message) where TMessage : IMessage
    {
        _currentResult ??= new MultiplePacketResult();

        _currentResult.Enqueue(new()
        {
            CmdType = cmdType,
            Head = Memory<byte>.Empty,
            Body = message.ToByteArray()
        });
    }

    protected IResult Response<TMessage>(CmdType cmdType, TMessage message) where TMessage : IMessage
    {
        NetPacket packet = new()
        {
            CmdType = cmdType,
            Head = Memory<byte>.Empty,
            Body = message.ToByteArray()
        };

        if (_currentResult != null)
        {
            _currentResult.Enqueue(packet);
            return _currentResult;
        }

        return new SinglePacketResult(packet);
    }
}
