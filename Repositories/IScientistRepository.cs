using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IScientistRepository
{
    Task<IEnumerable<Scientist>> GetAllScientistsAsync();
}