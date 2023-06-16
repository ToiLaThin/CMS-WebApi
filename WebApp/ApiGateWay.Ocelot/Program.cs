using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json", optional: true, reloadOnChange: true).Build();
string corPolicyName = "ocelotCorPolicy";
// Add services to the container.

builder.Services.AddOcelot(configuration);
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy(corPolicyName, (options) =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {}
app.UseCors(corPolicyName);
app.UseHttpsRedirection();
app.UseOcelot();
app.UseAuthorization();

app.MapControllers();

app.Run();
