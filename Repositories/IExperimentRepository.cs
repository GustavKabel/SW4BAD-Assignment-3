using AarhusSpaceProgram.Api.Entities;
namespace AarhusSpaceProgram.Api.Repositories;

public interface IExperimentRepository
{
    Task<IEnumerable<Experiment>> GetAllAsync();
    Task<Experiment?> GetByIdAsync(int id);
    Task<Experiment> CreateAsync(Experiment experiment);
    Task UpdateAsync(Experiment experiment);
    Task DeleteAsync(int id);
}