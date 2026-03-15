using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IMissionRepository
{
    Task<IEnumerable<Mission>> GetAllMissionsAsync();
}