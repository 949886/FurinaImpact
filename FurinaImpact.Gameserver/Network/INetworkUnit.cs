using System.Net;

namespace FurinaImpact.Gameserver.Network;
internal interface INetworkUnit : IDisposable
{
    IPEndPoint RemoteEndPoint { get; }

    ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken);
    ValueTask SendAsync(Memory<byte> buffer, CancellationToken cancellationToken);
}
