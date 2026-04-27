using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IMissionRepository
{
    Task<IEnumerable<Mission>> GetAllMissionsAsync();
    Task<Mission?> GetMissionByIdAsync(int id);
    Task<IEnumerable<Mission>> GetMissionsByTargetBodyAsync(string targetBodyName);
    Task<Mission> AddMissionAsync(Mission mission);
    Task UpdateMissionAsync(Mission mission);
    Task DeleteMissionAsync(int id);
    Task<bool> HasMissionOnDateAsync(int launchPadId, DateOnly date, int? excludeMissionId = null);
    Task<bool> AssignAstronautAsync(int missionId, int astronautId);
    Task<bool> RemoveAstronautAsync(int missionId, int astronautId);
    Task<bool> AssignScientistAsync(int missionId, int scientistId);
    Task<bool> RemoveScientistAsync(int missionId, int scientistId);
}