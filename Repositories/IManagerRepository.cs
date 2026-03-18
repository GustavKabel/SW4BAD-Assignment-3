using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface IManagerRepository
{
    Task<Manager> CreateManagerAsync(Manager manager);

    Task<Manager?> GetManagerByIdAsync(int id);

    Task UpdateManagerAsync(Manager manager);

    Task DeleteManagerAsync(int id);
}