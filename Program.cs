using Microsoft.EntityFrameworkCore;
using Serilog;
using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations;
using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Repositories;


var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IMissionRepository, MissionRepository>();
builder.Services.AddScoped<IScientistRepository, ScientistRepository>();

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    //.WriteTo.MongoDBBson(cfg =>
    //{
    //    cfg.SetMongoUrl(mongoConnectionString);
    //})
    .CreateLogger();

builder.Host.UseSerilog();

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<SpaceProgramContext>(options =>
    options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SpaceProgramContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database. Waiting for SQL Server...");
    }
}

// Enable OpenAPI + Scalar
app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

app.MapControllers();

app.Run();


