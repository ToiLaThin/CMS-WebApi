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
using System.Security.Claims;

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
                    cookieOption.Cookie.SameSite = SameSiteMode.None; //for angular can include asp.net core cookie in it request to received token
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, oidcConfig =>
                {
                    oidcConfig.Authority = "https://localhost:7134"; //URL OF IDENTITY SERVER
                    oidcConfig.ClientId = "client_id";
                    oidcConfig.ClientSecret = "client_secret";
                    oidcConfig.SaveTokens = true; //cookie have idtoken and acess token inside of it
                    oidcConfig.ResponseType = "code";

                    oidcConfig.Scope.Add("User.Info"); // require to get scope Usser Info
                    oidcConfig.Scope.Add("MyApi.Scope"); //request to this scope to get claim for access_token
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

app.MapGet("Auth/Redirect/IdentityServer4", (HttpContext ctx) =>
{
    return ctx.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties()
    {
        //RedirectUri = "Auth/Redirect/Token"
        //then from angular send a get request to get token remember to include the cookie

        //this will have a succsufull notification
        RedirectUri = "http://localhost:4200/auth/token"
    });
});

app.MapGet("Auth/Redirect/Token", [HttpGet] async (HttpContext ctx) =>
{
    var accessToken = await ctx.GetTokenAsync("access_token");
    var idToken = await ctx.GetTokenAsync("id_token");
    var claims = ctx.User.Claims.ToList();
    //mặc định httpClient angular ko include cookie vào get request => thêm withCredential: true
    //withCredential: true chỉ có 2 cookie Identity.Cookie và idsrv:sesison => phải set samesite = None?
    //gửi cả 6 cookie mới có access token và id token asp.net core cookie.c1 và asp.net core cookie.c2 => sucess
    var jwtIdToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
    var jwtAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
    ctx.Response.StatusCode = 222; //to identity  thís as special token request it 's 2xx which is still success
                                   //Response.Headers.AccessControlAllowOrigin;
                                   //Response.Headers.AccessControlAllowCredentials;
                                   //these two above will be set in middleware of ocelot so if we assign value to them, they will be overriden


    //once we safely get jwt token, we should delete all cookie
    //have to set expires and secure also and even domain, path, samesite, otherwise the set cookie = '' wwill not wowrk
    //see this  https://stackoverflow.com/a/48919470 and https://stackoverflow.com/a/63297464
    //if we just call Delete(cookieName), it will not work
    foreach (var cookieName in ctx.Request.Cookies.Keys)
    {
        //ctx.Response.Cookies.Append(cookieName, String.Empty);
        ctx.Response.Cookies.Delete(cookieName, new CookieOptions()
        {
            Domain = "localhost",
            Path = "/",
            //Expires = DateTime.Now.AddDays(-1),
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
    var cookies = ctx.Response.Cookies;
    return $"{accessToken};{idToken}";
});
app.Run();

