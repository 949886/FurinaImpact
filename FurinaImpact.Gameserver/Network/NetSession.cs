using System.Net;
using FurinaImpact.Common.Security;
using FurinaImpact.Gameserver.Controllers.Dispatching;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Protocol;
using Microsoft.Extensions.Logging;

namespace FurinaImpact.Gameserver.Network;
internal abstract class NetSession : IDisposable
{
    public IPEndPoint EndPoint => _networkUnit!.RemoteEndPoint;

    public long SessionId { get; private set; }
    private INetworkUnit? _networkUnit;

    private readonly ILogger _logger;
    private readonly NetSessionManager _sessionManager;
    private readonly NetCommandDispatcher _commandDispatcher;

    protected byte[] EncryptionKey { get; private set; }

    public NetSession(ILogger<NetSession> logger, NetSessionManager sessionManager, NetCommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _sessionManager = sessionManager;
        _commandDispatcher = commandDispatcher;

        EncryptionKey = MhySecurity.InitialKey;
    }

    public abstract ValueTask RunAsync();
    public abstract ValueTask SendAsync(NetPacket packet);

    public void Establish(long sessionId, INetworkUnit networkUnit)
    {
        SessionId = sessionId;
        _networkUnit = networkUnit;

        _sessionManager.Add(this);
    }

    protected async ValueTask<int> ConsumePacketsAsync(Memory<byte> buffer)
    {
        if (buffer.Length < 12)
            return 0;

        int consumed = 0;
        do
        {
            (NetPacket? packet, int bytesConsumed) = NetPacket.DecodeFrom(buffer[consumed..]);
            consumed += bytesConsumed;

            if (packet == null)
                return consumed;

            IResult? result = await _commandDispatcher.InvokeHandler(packet);
            if (result != null)
            {
                while (result.NextPacket(out NetPacket? serverPacket))
                {
                    await SendAsync(serverPacket);

                    if (serverPacket.CmdType == CmdType.GetPlayerTokenRsp)
                    {
                        InitializeEncryption(1337); // hardcoded MT seed with patch
                    }
                }
                
                _logger.LogInformation("Successfully handled command of type {cmdType}", packet.CmdType);
            }
        } while (buffer.Length - consumed >= 12);

        return consumed;
    }

    private void InitializeEncryption(ulong seed)
    {
        EncryptionKey = MhySecurity.GenerateSecretKey(seed);
    }

    protected async ValueTask<int> ReadWithTimeoutAsync(Memory<byte> buffer, int timeoutSeconds)
    {
        using CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(timeoutSeconds));
        return await _networkUnit!.ReceiveAsync(buffer, cancellationTokenSource.Token);
    }
    
    protected async ValueTask WriteWithTimeoutAsync(Memory<byte> buffer, int timeoutSeconds)
    {
        using CancellationTokenSource cancellationTokenSource = new(TimeSpan.FromSeconds(timeoutSeconds));
        await _networkUnit!.SendAsync(buffer, cancellationTokenSource.Token);
    }

    public virtual void Dispose()
    {
        _networkUnit?.Dispose();
        _ = _sessionManager.TryRemove(this);
    }
}
