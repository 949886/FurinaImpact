using System.Net;

namespace FurinaImpact.Gameserver.Options;
internal record GatewayOptions
{
    public const string Section = "Gateway";

    public required string Host { get; set; }
    public required int Port { get; set; }

    public IPEndPoint EndPoint => new(IPAddress.Parse(Host), Port);
}
