using Microsoft.EntityFrameworkCore;
using CMS.Post.DataConnect;

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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
