using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class MissionRepository : IMissionRepository
{
    private readonly SpaceProgramContext _context;

    public MissionRepository(SpaceProgramContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Mission>> GetAllMissionsAsync()
    {
        return await _context.Missions
            .Include(m => m.Manager)
            .Include(m => m.Rocket)
            .Include(m => m.LaunchPad)
            .Include(m => m.TargetBody)
            .Include(m => m.Astronauts)
            .ToListAsync();
    }
    public async Task<Mission?> GetMissionByIdAsync(int id)
    {
        return await _context.Missions
            .Include(m => m.Manager)
            .Include(m => m.Rocket)
            .Include(m => m.LaunchPad)
            .Include(m => m.TargetBody)
            .Include(m => m.Astronauts)
            .Include(m => m.Scientists)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.MissionId == id);
    }
    public async Task<IEnumerable<Mission>> GetMissionsByTargetBodyAsync(string targetBodyName)
    {
        return await _context.Missions
            .Include(m => m.Manager)
            .Include(m => m.Rocket)
            .Include(m => m.LaunchPad)
            .Include(m => m.TargetBody)
            .Include(m => m.Astronauts)
            .Where(m => m.TargetBody!.Name == targetBodyName)
            .ToListAsync();
    }
}