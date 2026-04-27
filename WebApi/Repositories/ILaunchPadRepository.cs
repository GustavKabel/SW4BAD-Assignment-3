using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Repositories;

public interface ILaunchPadRepository
{
    Task<IEnumerable<LaunchPad>> GetAllLaunchPadsAsync();
    Task<LaunchPad?> GetLaunchPadByIdAsync(int id);
    Task<LaunchPad> AddLaunchPadAsync(LaunchPad launchPad);
    Task UpdateLaunchPadAsync(LaunchPad launchPad);
    Task DeleteLaunchPadAsync(int id);
}