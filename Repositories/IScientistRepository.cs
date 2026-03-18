using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IScientistRepository
{
    Task<IEnumerable<Scientist>> GetAllScientistsAsync();

    Task<Scientist> CreateScientistAsync(Scientist scientist);

    Task<Scientist?> GetScientistByIdAsync(int id);

    Task UpdateScientistAsync(Scientist scientist);

    Task DeleteScientistAsync(int id);
}