using FurinaImpact.Protocol;

namespace FurinaImpact.Gameserver.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal class NetCommandAttribute : Attribute
{
    public CmdType CmdType { get; }

    public NetCommandAttribute(CmdType cmdType)
    {
        CmdType = cmdType;
    }
}
