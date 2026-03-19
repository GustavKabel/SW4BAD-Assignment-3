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
builder.Services.AddScoped<IAstronautRepository, AstronautRepository>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRocketRepository, RocketRepository>();
builder.Services.AddScoped<ILaunchPadRepository, LaunchPadRepository>();

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
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

app.Use(async (context, next) =>
{
    // Let the request be processed by the API first
    await next(); 

    var method = context.Request.Method;

    // Check if it's a modifying request (POST, PUT, DELETE)
    if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsDelete(method))
    {
        // Get the logger
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        
        // Log the required details (Timestamp is automatically added by Serilog)
        logger.LogInformation(
            "Action: {HttpMethod} {RequestPath} | Status: {StatusCode}", 
            method, 
            context.Request.Path, 
            context.Response.StatusCode);
    }
});
// ---------------------------

app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

app.MapControllers();

app.Run();


