using Microsoft.EntityFrameworkCore;
using CMS.Post.DataConnect;
using CMS.Post.Api;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CMS.Helper; //ref by post.api.service
using CMS.Helper.StaticClass; 

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                       .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                                       .Build();
string? cmsConnStr = config.GetConnectionString("CMS_db");

// Add services to the container.
builder.Services.AddDbContext<PostContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(cmsConnStr,
        sqlServerBuilderAction =>
        {
            sqlServerBuilderAction.MigrationsHistoryTable("__EFMigrationHistory", "Post");
            //sqlServerBuilderAction.MigrationsAssembly("CMS.Post.DataConnect"); since we run migration on DataConnect
        }
    );
});
builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOption =>
                {
                    jwtOption.Authority = "https://localhost:7134"; //TO FIX
                    jwtOption.SaveToken = true;
                    jwtOption.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateActor = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

builder.Services.AddAuthorization(authOption =>
{
    authOption.AddPolicy(PolicyNames.AdminPolicy, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireRole("admin");
    });
    authOption.AddPolicy(PolicyNames.AuthenticatedUserPolicy, policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireRole("authenticatedUser");
    });
});

builder.Services.AddControllers();

builder.Services.PostDependencyInjection();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string corPolicyName = "thinhnd";
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy(corPolicyName, (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddAutoMapper(assemblies: Assembly.GetExecutingAssembly()); //TODO try different options
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corPolicyName);
app.UseAuthentication();
//app.UseStatusCodePages(async context => {
//    var request = context.HttpContext.Request;
//    var response = context.HttpContext.Response;
//    if (response.StatusCode == (int)HttpStatusCode.Unauthorized) {
//        response.Redirect("http://localhost:4200/"); //Redirect to angular but will have cors issue in frontEnd -> solution is use proxy(learn more if have time)
//    }
//});
app.UseAuthorization();

app.MapControllers();

app.Run();
