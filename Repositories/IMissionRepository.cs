using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IMissionRepository
{
    Task<IEnumerable<Mission>> GetAllMissionsAsync();
    Task<Mission?> GetMissionByIdAsync(int id);
    Task<IEnumerable<Mission>> GetMissionsByTargetBodyAsync(string targetBodyName);
}