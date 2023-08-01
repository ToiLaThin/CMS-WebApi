using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddAuthentication(authConfig =>
{
    authConfig.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authConfig.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cookieOption =>
                {
                    //cookieOption.LoginPath = "/login/identityServer4";
                    //cookieOption.AccessDeniedPath = "/unauthorized";
                    //secret api use cookie authentication
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, oidcConfig =>
                {
                    oidcConfig.Authority = "https://localhost:7134"; //URL OF IDENTITY SERVER
                    oidcConfig.ClientId = "client_id";
                    oidcConfig.ClientSecret = "client_secret";
                    oidcConfig.SaveTokens = true; //cookie have idtoken and acess token inside of it
                    oidcConfig.ResponseType = "code";
                });
builder.Services.AddAuthorization();
string corPolicyName = "thinhnd"; //add cors
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy(corPolicyName, (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors(corPolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("Auth/Redirect/IdentityServer4", (HttpContext ctx) => {
    return ctx.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties()
    {
        //RedirectUri = "Auth/Redirect/Token"
        //then from angular send a get request to get token remember to include the cookie

        //this will have a succsufull notification
        RedirectUri = "http://localhost:4200/"
    });
});

app.MapGet("Auth/Redirect/Token",[HttpGet] async (HttpContext ctx) =>
{
    var accessToken = await ctx.GetTokenAsync("access_token");
    var idToken = await ctx.GetTokenAsync("id_token");
    //mặc định httpClient angular ko include cookie vào get request => thêm withCredential: true
    //withCredential: true chỉ có 2 cookie Identity.Cookie và idsrv:sesison
    //gửi cả 6 cookie mới có access token và id token asp.net core cookie.c1 và asp.net core cookie.c2
    var jwtIdToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
    var jwtAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
    return accessToken.ToString() + "\n" + idToken.ToString();
});
app.Run();

