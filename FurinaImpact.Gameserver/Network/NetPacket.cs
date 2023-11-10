using System.Buffers.Binary;
using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Network;
internal class NetPacket
{
    private const ushort HeadMagic = 0x4567;
    private const ushort TailMagic = 0x89AB;

    public CmdType CmdType { get; set; }
    public Memory<byte> Head { get; set; }
    public Memory<byte> Body { get; set; }

    public int EncodeTo(Memory<byte> buffer)
    {
        Span<byte> span = buffer.Span;

        BinaryPrimitives.WriteUInt16BigEndian(span[0..2], HeadMagic);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..4], (ushort)CmdType);
        BinaryPrimitives.WriteUInt16BigEndian(span[4..6], (ushort)Head.Length);
        BinaryPrimitives.WriteInt32BigEndian(span[6..10], Body.Length);
        Head.CopyTo(buffer[10..]);
        Body.CopyTo(buffer[(10 + Head.Length)..]);
        BinaryPrimitives.WriteUInt16BigEndian(span[(10 + Head.Length + Body.Length)..], TailMagic);

        return 12 + Head.Length + Body.Length;
    }

    public static (NetPacket?, int) DecodeFrom(Memory<byte> data)
    {
        ReadOnlySpan<byte> span = data.Span;

        ushort headMagic = BinaryPrimitives.ReadUInt16BigEndian(span[0..2]);
        if (headMagic != HeadMagic)
            return (null, 0);

        ushort cmdType = BinaryPrimitives.ReadUInt16BigEndian(span[2..4]);

        int headLength = BinaryPrimitives.ReadUInt16BigEndian(span[4..6]);
        int bodyLength = BinaryPrimitives.ReadInt32BigEndian(span[6..10]);

        if (data.Length < 12 + headLength + bodyLength)
            return (null, 0);

        Memory<byte> head = data.Slice(10, headLength);
        Memory<byte> body = data.Slice(10 + headLength, bodyLength);

        ushort tailMagic = BinaryPrimitives.ReadUInt16BigEndian(span[(10 + headLength + bodyLength)..]);
        if (tailMagic != TailMagic)
            return (null, 0);

        NetPacket netPacket = new()
        {
            CmdType = (CmdType)cmdType,
            Head = head,
            Body = body
        };

        return (netPacket, 12 + headLength + bodyLength);
    }
}
