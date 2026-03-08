using Microsoft.EntityFrameworkCore;
using Serilog;
using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations;


// stuff

var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.MongoDBBson(cfg => 
    {
        cfg.SetMongoUrl(mongoConnectionString);
    })
    .CreateLogger();

builder.Host.UseSerilog();

var sqlConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<SpaceProgramContext>(options =>
    options.UseSqlServer(sqlConnectionString));


var app = builder.Build();
// Enable OpenAPI + Scalar
app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

    public class SpaceProgramContext : DbContext
    {
        public SpaceProgramContext(DbContextOptions options) : base(options) { }
    }

