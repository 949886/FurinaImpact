using System.Buffers;
using System.Net;
using System.Net.Sockets;
using FurinaImpact.Gameserver.Options;
using FurinaImpact.Kcp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FurinaImpact.Gameserver.Network.Kcp;
internal sealed class KcpGateway : IGateway
{
    private readonly Random _random;
    private readonly ILogger _logger;
    private readonly IOptions<GatewayOptions> _options;
    private readonly NetSessionManager _sessionManager;

    private uint _sessionCounter;
    private IKcpTransport<IKcpMultiplexConnection>? _transport;

    public KcpGateway(ILogger<KcpGateway> logger, IOptions<GatewayOptions> options, NetSessionManager sessionManager)
    {
        _logger = logger;
        _options = options;
        _sessionManager = sessionManager;

        _random = new();
    }

    public Task Start()
    {
        IPEndPoint bindEndPoint = _options.Value.EndPoint;

        _transport = KcpSocketTransport.CreateMultiplexConnection(new(bindEndPoint), 1400);
        _transport.SetCallbacks(20, HandleKcpHandshake);
        _transport.Start();

        _logger.LogInformation("KCP Gateway is up at {endPoint}", bindEndPoint);
        return Task.CompletedTask;
    }

    private async ValueTask HandleKcpHandshake(UdpReceiveResult receiveResult)
    {
        KcpHandshake handshake = KcpHandshake.ReadFrom(receiveResult.Buffer);
        
        switch ((handshake.Head, handshake.Tail))
        {
            case (KcpHandshake.StartConversationHead, KcpHandshake.StartConversationTail):
                await OnStartConversationRequest(receiveResult.RemoteEndPoint);
                break;
        }
    }

    private async ValueTask OnStartConversationRequest(IPEndPoint clientEndPoint)
    {
        uint convId = Interlocked.Increment(ref _sessionCounter);
        uint token  = (uint)_random.Next();

        long convId64 = (long)convId << 32 | token;
        KcpConversation conversation = _transport!.Connection.CreateConversation(convId64, clientEndPoint);
        _ = _sessionManager.RunSessionAsync(convId64, new KcpNetworkUnit(conversation, clientEndPoint));

        await SendConversationCreatedPacket(clientEndPoint, convId, token);
    }

    private async ValueTask SendConversationCreatedPacket(IPEndPoint clientEndPoint, uint convId, uint token)
    {
        KcpHandshake handshakeResponse = new()
        {
            Head   = KcpHandshake.ConversationCreatedHead,
            Param1 = convId,
            Param2 = token,
            Data   = 1234567890,
            Tail   = KcpHandshake.ConversationCreatedTail
        };

        byte[] buffer = ArrayPool<byte>.Shared.Rent(20);
        try
        {
            Memory<byte> bufferMemory = buffer.AsMemory();

            handshakeResponse.WriteTo(buffer);
            await _transport!.SendPacketAsync(bufferMemory[..20], clientEndPoint, CancellationToken.None);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    public Task Stop()
    {
        return Task.CompletedTask;
    }
}
