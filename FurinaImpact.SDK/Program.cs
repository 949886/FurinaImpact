using FurinaImpact.SDK.Handlers;

Console.Title = "FurinaImpact | SDK Server";
Directory.SetCurrentDirectory(AppContext.BaseDirectory);

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8888");

WebApplication app = builder.Build();

app.MapGet("/query_region_list", RegionHandler.OnQueryRegionList);
app.MapGet("/query_cur_region", RegionHandler.OnQueryCurRegion);

app.MapPost("/account/risky/api/check",                     AuthHandler.OnCaptchaRequest);
app.MapPost("/{product_name}/mdk/shield/api/login",         AuthHandler.OnPasswordLogin);
app.MapPost("/{product_name}/combo/granter/login/v2/login", AuthHandler.OnGranterLogin);
    
await app.RunAsync();
