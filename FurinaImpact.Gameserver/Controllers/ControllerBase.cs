using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Network;
using FurinaImpact.Protocol;
using Google.Protobuf;

namespace FurinaImpact.Gameserver.Controllers;
internal abstract class ControllerBase
{
    public NetPacket? Packet { get; set; }

    protected IResult Ok()
    {
        return new SinglePacketResult(null);
    }

    protected IResult Response<TMessage>(CmdType cmdType, TMessage message) where TMessage : IMessage
    {
        return new SinglePacketResult(new()
        {
            CmdType = cmdType,
            Head = Memory<byte>.Empty,
            Body = message.ToByteArray()
        });
    }
}
