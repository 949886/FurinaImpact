using FurinaImpact.Gameserver;
using FurinaImpact.Gameserver.Controllers.Dispatching;
using FurinaImpact.Gameserver.Network;
using FurinaImpact.Gameserver.Network.Kcp;
using FurinaImpact.Gameserver.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.Title = "FurinaImpact | Game Server [Experimental]";

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Logging.AddSimpleConsole();

builder.Services.Configure<GatewayOptions>(builder.Configuration.GetSection(GatewayOptions.Section));

builder.Services.AddScoped<NetCommandDispatcher>();
builder.Services.AddScoped<NetSession, KcpSession>();
builder.Services.AddSingleton<IGateway, KcpGateway>();
builder.Services.AddSingleton<NetSessionManager>();

builder.Services.AddHostedService<GameServer>();

await builder.Build().RunAsync();