using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IAstronautRepository
{
    Task<IEnumerable<Astronaut>> GetAstronautsByExperienceAsync();

    Task<Astronaut> CreateAstronautAsync(Astronaut astronaut);

    Task<Astronaut?> GetAstronautByIdAsync(int id);

    Task UpdateAstronautAsync(Astronaut astronaut);

    Task DeleteAstronautAsync(int id);
}