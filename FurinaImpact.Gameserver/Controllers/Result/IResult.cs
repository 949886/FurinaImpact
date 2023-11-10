using System.Diagnostics.CodeAnalysis;
using FurinaImpact.Gameserver.Network;

namespace FurinaImpact.Gameserver.Controllers.Result;
internal interface IResult
{
    bool NextPacket([MaybeNullWhen(false)] out NetPacket packet);
}
