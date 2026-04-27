using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class LaunchPadRepository : ILaunchPadRepository
{
    private readonly SpaceProgramContext _context;

    public LaunchPadRepository(SpaceProgramContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LaunchPad>> GetAllLaunchPadsAsync()
    {
        return await _context.LaunchPads.ToListAsync();
    }

    public async Task<LaunchPad?> GetLaunchPadByIdAsync(int id)
    {
        return await _context.LaunchPads.FindAsync(id);
    }

    public async Task<LaunchPad> AddLaunchPadAsync(LaunchPad launchPad)
    {
        _context.LaunchPads.Add(launchPad);
        await _context.SaveChangesAsync();
        return launchPad;
    }

    public async Task UpdateLaunchPadAsync(LaunchPad launchPad)
    {
        _context.LaunchPads.Update(launchPad);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLaunchPadAsync(int id)
    {
        var launchPad = await _context.LaunchPads.FindAsync(id);
        if (launchPad != null)
        {
            _context.LaunchPads.Remove(launchPad);
            await _context.SaveChangesAsync();
        }
    }
}