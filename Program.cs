using Microsoft.EntityFrameworkCore;
using Serilog;
using Scalar.AspNetCore;
using System.ComponentModel.DataAnnotations;
using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Repositories;
using Microsoft.AspNetCore.Identity;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        });

        return Task.CompletedTask;
    });
});

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

//Add requirements to the password
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<SpaceProgramContext>();

//Add authentication service
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
    System.Text.Encoding.UTF8.GetBytes(
    builder.Configuration["JWT:SigningKey"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    // read-only
    options.AddPolicy("ReadOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Astronaut", "Manager"));

    // Anyone can view experiments
    options.AddPolicy("CanViewExperiments", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Astronaut", "Scientist", "Manager"));

    // Only Scientists and Managers can modify experiments
    options.AddPolicy("CanModifyExperiments", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Scientist", "Manager"));

    // full access to everything
    options.AddPolicy("ManagerFullAccess", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Manager"));
});

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

        await SeedData.InitializeAsync(services);
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

app.UseAuthentication();
app.UseAuthorization();
app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

app.MapControllers();

app.Run();


