namespace FurinaImpact.Gameserver.Network;
internal interface IGateway
{
    Task Start();
    Task Stop();
}
