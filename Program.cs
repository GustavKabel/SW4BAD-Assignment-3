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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

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
    await next(); 

    var method = context.Request.Method;

    if (HttpMethods.IsPost(method) || HttpMethods.IsPut(method) || HttpMethods.IsDelete(method))
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation(
            "Action: {HttpMethod} {RequestPath} | Status: {StatusCode}", 
            method, 
            context.Request.Path, 
            context.Response.StatusCode);
    }
});

app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

app.MapControllers();

app.Run();


