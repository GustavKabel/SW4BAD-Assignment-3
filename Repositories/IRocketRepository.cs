using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IRocketRepository
{
    Task<IEnumerable<Rocket>> GetAllRocketsAsync();
    Task<Rocket?> GetRocketByIdAsync(int id);
    Task<Rocket> AddRocketAsync(Rocket rocket);
    Task UpdateRocketAsync(Rocket rocket);
    Task DeleteRocketAsync(int id);
}