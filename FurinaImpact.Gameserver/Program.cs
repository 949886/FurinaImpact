using FurinaImpact.Common.Data.Excel;
using FurinaImpact.Common.Data.Provider;
using FurinaImpact.Gameserver;
using FurinaImpact.Gameserver.Controllers.Dispatching;
using FurinaImpact.Gameserver.Game;
using FurinaImpact.Gameserver.Game.Avatar;
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

// Resources
builder.Services.UseLocalAssets();
builder.Services.AddSingleton<ExcelTableCollection>();

// Game Logic
builder.Services.AddScoped<Player>();

// Network
builder.Services.AddScoped<NetCommandDispatcher>();
builder.Services.AddScoped<NetSession, KcpSession>();
builder.Services.AddSingleton<IGateway, KcpGateway>();
builder.Services.AddSingleton<NetSessionManager>();

builder.Services.AddHostedService<GameServer>();

await builder.Build().RunAsync();