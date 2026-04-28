using AarhusSpaceProgram.Api.loggingModels;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IMissionLogRepository
{
    Task<IEnumerable<MissionLog>> GetLogsByMissionIdAsync(int missionId);
}