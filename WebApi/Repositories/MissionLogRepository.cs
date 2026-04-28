using AarhusSpaceProgram.Api.loggingModels;
using MongoDB.Driver;

namespace AarhusSpaceProgram.Api.Repositories;

public class MissionLogRepository : IMissionLogRepository
{
    private readonly IMongoCollection<MissionLog> _logs;

    public MissionLogRepository(IMongoDatabase database)
    {
        _logs = database.GetCollection<MissionLog>("MissionLogs");
    }

    public async Task<IEnumerable<MissionLog>> GetLogsByMissionIdAsync(int missionId)
    {
        var filter = Builders<MissionLog>.Filter.Eq(l => l.MissionId, missionId);
        var sort = Builders<MissionLog>.Sort.Descending(l => l.Timestamp);

        return await _logs.Find(filter).Sort(sort).ToListAsync();
    }
}