using MissionLogGenerator;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);



// 1. Configure MongoDB
// It looks for the connection string in appsettings.json or environment variables (Docker)
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb") 
    ?? "mongodb://localhost:27017";

builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

// 2. Configure HttpClient to talk to your Web API
// It looks for the Base URL in appsettings.json or environment variables (Docker)
var webApiBaseUrl = builder.Configuration["WebApiBaseUrl"] ?? "http://localhost:8080";

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(webApiBaseUrl);
});

// 3. Register the Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();