using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IAstronautRepository
{
    Task<IEnumerable<Astronaut>> GetAstronautsByExperienceAsync();

    Task<Astronaut> CreateAstronautAsync(Astronaut astronaut);
}