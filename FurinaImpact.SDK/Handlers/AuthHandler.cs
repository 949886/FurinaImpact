using System.Text.Json;
using FurinaImpact.SDK.Models;

namespace FurinaImpact.SDK.Handlers;

public static class AuthHandler
{
    public static IResult OnPasswordLogin()
    {
        // TODO: account system

        ShieldApiAuthResponse response = new()
        {
            Data = new()
            {
                Account = new()
                {
                    AreaCode = "**",
                    Country = "RU",
                    Email = "FurinaImpact",
                    IsEmailVerify = "1",
                    Token = "mostsecuretokenever",
                    Uid = "1337"
                }
            }
        };

        return JsonStringResult(JsonSerializer.Serialize(response));
    }

    public static async Task<IResult> OnGranterLogin(HttpRequest httpRequest)
    {
        GranterLoginRequest? request = await httpRequest.ReadFromJsonAsync<GranterLoginRequest>();
        if (request is null) return Results.BadRequest();

        GranterLoginRequest.RequestData? data = JsonSerializer.Deserialize<GranterLoginRequest.RequestData>(request.Data);
        if (data is null) return Results.BadRequest();

        string? openId = data.OpenId;
        string? token = data.Token;

        return JsonStringResult($$"""{"retcode":0,"message":"OK","data":{"combo_id":1,"open_id":{{openId}},"combo_token":"{{token}}","data":"{\"guest\":false}","heartbeat":false,"account_type":1,"fatigue_remind":null}""");
    }

    public static IResult OnCaptchaRequest() =>
        JsonStringResult("""{"data":{"id":"06611ed14c3131a676b19c0d34c0644b","action":"ACTION_NONE","geetest":null},"message":"OK","retcode":0}""");

    private static IResult JsonStringResult(string jsonString) =>
        TypedResults.Text(jsonString, "application/json");
}
