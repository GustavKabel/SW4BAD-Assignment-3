using MissionLogGenerator;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb") 
    ?? "mongodb://localhost:27017";

builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

var webApiBaseUrl = builder.Configuration["WebApiBaseUrl"] ?? "http://localhost:8080";

builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(webApiBaseUrl);
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();