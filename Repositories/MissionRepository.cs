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

    public async Task<Mission> AddMissionAsync(Mission mission)
    {
        _context.Missions.Add(mission);
        await _context.SaveChangesAsync();
        return mission;
    }

    public async Task UpdateMissionAsync(Mission mission)
    {
        _context.Missions.Update(mission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMissionAsync(int id)
    {
        var mission = await _context.Missions.FindAsync(id);
        if (mission != null)
        {
            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
        }
    }

    
    public async Task<bool> HasMissionOnDateAsync(int launchPadId, DateOnly date, int? excludeMissionId = null)
    {
        var query = _context.Missions.Where(m => m.LaunchPadId == launchPadId && m.PlannedLaunchDate == date);
        
        
        if (excludeMissionId.HasValue)
        {
            query = query.Where(m => m.MissionId != excludeMissionId.Value);
        }

        return await query.AnyAsync();
    }
}