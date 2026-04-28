using System.Net.Http.Json;
using MissionLogGenerator.DTOs;
using MissionLogGenerator.Models;
using MongoDB.Driver;

namespace MissionLogGenerator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMongoCollection<MissionLog> _logsCollection;

    public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IMongoClient mongoClient)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        
        var database = mongoClient.GetDatabase("AarhusSpaceLogs");
        _logsCollection = database.GetCollection<MissionLog>("MissionLogs");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await ProcessActiveMissionsAsync(stoppingToken);
        }
    }

    private async Task ProcessActiveMissionsAsync(CancellationToken stoppingToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            
            var response = await client.GetAsync("/api/Missions?status=Active", stoppingToken);
            
            if (response.IsSuccessStatusCode)
            {
                var activeMissions = await response.Content.ReadFromJsonAsync<List<MissionDto>>(cancellationToken: stoppingToken);

                if (activeMissions != null && activeMissions.Any())
                {
                    var logsToInsert = new List<MissionLog>();

                    foreach (var mission in activeMissions)
                    {
                        logsToInsert.Add(new MissionLog
                        {
                            MissionId = mission.MissionId,
                            Message = $"Telemetry check completed for mission {mission.MissionId}",
                            Timestamp = DateTime.UtcNow
                        });
                    }

                    if (logsToInsert.Any())
                    {
                        await _logsCollection.InsertManyAsync(logsToInsert, cancellationToken: stoppingToken);
                        _logger.LogInformation("Successfully inserted {Count} mission logs.", logsToInsert.Count);
                    }
                }
                else
                {
                    _logger.LogInformation("No active missions found at this time.");
                }
            }
            else
            {
                _logger.LogWarning("Failed to fetch active missions. Status code: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating mission logs.");
        }
    }
}