using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using FurinaImpact.Gameserver.Controllers.Attributes;
using FurinaImpact.Gameserver.Controllers.Result;
using FurinaImpact.Gameserver.Network;
using FurinaImpact.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FurinaImpact.Gameserver.Controllers.Dispatching;
internal class NetCommandDispatcher
{
    private static readonly ImmutableDictionary<CmdType, Func<IServiceProvider, NetPacket, ValueTask<IResult>>> _handlers;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    static NetCommandDispatcher()
    {
        _handlers = InitializeHandlers();
    }

    public NetCommandDispatcher(IServiceProvider serviceProvider, ILogger<NetCommandDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async ValueTask<IResult?> InvokeHandler(NetPacket packet)
    {
        ArgumentNullException.ThrowIfNull(packet, nameof(packet));

        if (_handlers.TryGetValue(packet.CmdType, out Func<IServiceProvider, NetPacket, ValueTask<IResult>>? handler))
        {
            return await handler(_serviceProvider, packet);
        }

        _logger.LogWarning("No handler defined for command of type {cmdType}", packet.CmdType);
        return null;
    }

    private static ImmutableDictionary<CmdType, Func<IServiceProvider, NetPacket, ValueTask<IResult>>> InitializeHandlers()
    {
        ImmutableDictionary<CmdType, Func<IServiceProvider, NetPacket, ValueTask<IResult>>>.Builder builder
            = ImmutableDictionary.CreateBuilder<CmdType, Func<IServiceProvider, NetPacket, ValueTask<IResult>>>();

        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => type.GetCustomAttribute<NetControllerAttribute>() != null);

        foreach (Type type in types)
        {
            IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                              .Where(method => method.GetCustomAttribute<NetCommandAttribute>() != null);

            foreach (MethodInfo method in methods)
            {
                NetCommandAttribute attribute = method.GetCustomAttribute<NetCommandAttribute>()!;
                if (builder.TryGetKey(attribute.CmdType, out _))
                    throw new Exception($"Handler for command {attribute.CmdType} defined twice!");

                builder[attribute.CmdType] = CreateHandlerDelegate(type, method);
            }
        }

        return builder.ToImmutable();
    }

    private static Func<IServiceProvider, NetPacket, ValueTask<IResult>> CreateHandlerDelegate(Type controllerType, MethodInfo method)
    {
        ParameterExpression serviceProviderParameter = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
        ParameterExpression netPacketParameter = Expression.Parameter(typeof(NetPacket), "packet");

        ConstantExpression controllerTypeConstant = Expression.Constant(controllerType);
        ParameterExpression controllerVariable = Expression.Variable(controllerType, "controller");
        PropertyInfo packetProperty = typeof(ControllerBase).GetProperty("Packet")!;

        MethodInfo createInstanceMethod = typeof(ActivatorUtilities).GetMethod("CreateInstance", new Type[] { typeof(IServiceProvider), typeof(Type), typeof(object[]) })!;

        List<Expression> expressionBlock = new()
        {
            Expression.Assign(controllerVariable, Expression.Convert(
                Expression.Call(null, createInstanceMethod, serviceProviderParameter, controllerTypeConstant, Expression.Constant(Array.Empty<object>())),
                controllerType)),
            Expression.Assign(Expression.Property(controllerVariable, packetProperty), netPacketParameter)
        };

        List<Expression> parameterExpressions = new();
        foreach (ParameterInfo parameter in method.GetParameters())
        {
            MethodInfo getServiceMethod = typeof(ServiceProviderServiceExtensions)
                .GetMethod("GetRequiredService", new Type[] { typeof(IServiceProvider) })!
                .MakeGenericMethod(parameter.ParameterType);

            parameterExpressions.Add(Expression.Call(getServiceMethod, serviceProviderParameter));
        }

        expressionBlock.Add(Expression.Call(controllerVariable, method, parameterExpressions));

        return Expression.Lambda<Func<IServiceProvider, NetPacket, ValueTask<IResult>>>(
            Expression.Block(new[] { controllerVariable }, expressionBlock),
            serviceProviderParameter,
            netPacketParameter)
            .Compile();
    }
}
