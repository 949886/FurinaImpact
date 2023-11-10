using System.Buffers.Binary;

namespace FurinaImpact.Gameserver.Network.Kcp;

internal struct KcpHandshake
{
    public const uint StartConversationHead = 0xFF;
    public const uint StartConversationTail = 0xFFFFFFFF;

    public const uint ConversationCreatedHead = 0x00000145;
    public const uint ConversationCreatedTail = 0x14514545;

    public const uint ConversationEndHead = 0x194;
    public const uint ConversationEndTail = 0x19419494;

    public uint Head { get; set; }
    public uint Param1 { get; set; }
    public uint Param2 { get; set; }
    public uint Data { get; set; }
    public uint Tail { get; set; }

    public readonly void WriteTo(Span<byte> buffer)
    {
        BinaryPrimitives.WriteUInt32BigEndian(buffer[0..4],     Head);
        BinaryPrimitives.WriteUInt32LittleEndian(buffer[4..8],  Param1);
        BinaryPrimitives.WriteUInt32LittleEndian(buffer[8..12], Param2);
        BinaryPrimitives.WriteUInt32BigEndian(buffer[12..16],   Data);
        BinaryPrimitives.WriteUInt32BigEndian(buffer[16..20],   Tail);
    }

    public static KcpHandshake ReadFrom(ReadOnlySpan<byte> buffer) => new()
    {
        Head   = BinaryPrimitives.ReadUInt32BigEndian(buffer[0..4]),
        Param1 = BinaryPrimitives.ReadUInt32LittleEndian(buffer[4..8]),
        Param2 = BinaryPrimitives.ReadUInt32LittleEndian(buffer[8..12]),
        Data   = BinaryPrimitives.ReadUInt32BigEndian(buffer[12..16]),
        Tail   = BinaryPrimitives.ReadUInt32BigEndian(buffer[16..20])
    };
}
