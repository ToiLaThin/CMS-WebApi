using CMS.Helper.OptionPatternClasses;
using CMS.Helper.SharedServices;
using IdentityServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//config option pattern for email sender TODO refactor for better structure, extension maybe?
IConfiguration emailConfiguration = builder.Configuration.GetSection("MailOptions");
builder.Services.Configure<MailOptions>(emailConfiguration);
builder.Services.AddTransient<EmailSenderService, EmailSenderService>();

//config asp.net core identity for storing user infomation
string connString = builder.Configuration.GetConnectionString("CMS_db");
builder.Services.AddDbContext<IdentityDbContext>(identityDbConfig =>
{
    identityDbConfig.UseSqlServer(connString, sqlServerConfig =>
    {
        sqlServerConfig.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
        sqlServerConfig.MigrationsHistoryTable("__EFMigrationHistory", "Identity");
    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(identityConfig =>
{
    identityConfig.Password.RequiredLength = 4;
    identityConfig.Password.RequireDigit = false;
    identityConfig.Password.RequireUppercase = false;
    identityConfig.Password.RequireNonAlphanumeric = false;

    //config to require confirmed email
    identityConfig.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddSignInManager<SignInManager<IdentityUser>>()
    .AddDefaultTokenProviders(); //for token confirm email generation
//configure cookie to store identity server session
builder.Services.ConfigureApplicationCookie(cookieConfig =>
{
    cookieConfig.Cookie.Name = "Identity.Cookie"; //=> cookie represent authenticated with idenityserver via google diff from Identity.External
    cookieConfig.LoginPath = "/Auth/Login";
});


builder.Services.AddIdentityServer(identityServerOption =>
{
    identityServerOption.UserInteraction.LoginUrl = "/Auth/Login";
})
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryClients(IdentityServerConfiguration.GetClients())
    .AddInMemoryApiResources(IdentityServerConfiguration.GetApis())
    .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentities())
    .AddInMemoryApiScopes(IdentityServerConfiguration.GetScopes())
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication().AddGoogle("Google", googleOption =>
{
    googleOption.ClientId = builder.Configuration.GetSection("Authentication:Google:ClientId").Value;
    googleOption.ClientSecret = builder.Configuration.GetSection("Authentication:Google:ClientSecret").Value;
    googleOption.SignInScheme = IdentityConstants.ExternalScheme; //Identity.External will be default => cookie represent authenticated with google
                                                                  //googleOption.SaveTokens = true; //để lấy access token trong callback uri
});
string corPolicyName = "thinhnd"; //add cors
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy(corPolicyName, (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseCors(corPolicyName);
app.UseIdentityServer();
app.MapDefaultControllerRoute();
app.Run();

