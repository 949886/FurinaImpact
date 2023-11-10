using System.Net;
using FurinaImpact.Kcp;

namespace FurinaImpact.Gameserver.Network.Kcp;
internal class KcpNetworkUnit : INetworkUnit
{
    public IPEndPoint RemoteEndPoint { get; }

    private readonly KcpConversation _conversation;

    public KcpNetworkUnit(KcpConversation conversation, IPEndPoint remoteEndPoint)
    {
        _conversation = conversation;
        RemoteEndPoint = remoteEndPoint;
    }

    public async ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        KcpConversationReceiveResult result = await _conversation.ReceiveAsync(buffer, cancellationToken);
        if (result.TransportClosed)
            return -1;

        return result.BytesReceived;
    }

    public async ValueTask SendAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        await _conversation.SendAsync(buffer, cancellationToken);
    }

    public void Dispose()
    {
        _conversation.Dispose();
    }
}
