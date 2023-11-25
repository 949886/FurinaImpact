using FurinaImpact.Common.Data;
using FurinaImpact.Common.Data.Binout;
using FurinaImpact.Common.Data.Excel;
using FurinaImpact.Common.Data.Provider;
using FurinaImpact.Gameserver;
using FurinaImpact.Gameserver.Controllers.Dispatching;
using FurinaImpact.Gameserver.Game;
using FurinaImpact.Gameserver.Game.Entity;
using FurinaImpact.Gameserver.Game.Entity.Factory;
using FurinaImpact.Gameserver.Game.Entity.Listener;
using FurinaImpact.Gameserver.Game.Scene;
using FurinaImpact.Gameserver.Network;
using FurinaImpact.Gameserver.Network.Kcp;
using FurinaImpact.Gameserver.Network.Session;
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
builder.Services.AddSingleton<DataHelper>();
builder.Services.AddSingleton<ExcelTableCollection>();
builder.Services.AddSingleton<BinDataCollection>();

// Game Logic
builder.Services.AddScoped<Player>();
builder.Services.AddScoped<SceneManager>();
builder.Services.AddScoped<EntityManager>();
builder.Services.AddScoped<EntityFactory>();

// Logic Listeners
builder.Services.AddScoped<IEntityEventListener, SessionEntityEventListener>();

// Network
builder.Services.AddScoped<NetCommandDispatcher>();
builder.Services.AddScoped<NetSession, KcpSession>();
builder.Services.AddSingleton<IGateway, KcpGateway>();
builder.Services.AddSingleton<NetSessionManager>();

builder.Services.AddHostedService<GameServer>();

await builder.Build().RunAsync();