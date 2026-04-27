using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class RocketRepository : IRocketRepository
{
    private readonly SpaceProgramContext _context;

    public RocketRepository(SpaceProgramContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rocket>> GetAllRocketsAsync()
    {
        return await _context.Rockets.ToListAsync();
    }

    public async Task<Rocket?> GetRocketByIdAsync(int id)
    {
        return await _context.Rockets.FindAsync(id);
    }

    public async Task<Rocket> AddRocketAsync(Rocket rocket)
    {
        _context.Rockets.Add(rocket);
        await _context.SaveChangesAsync();
        return rocket;
    }

    public async Task UpdateRocketAsync(Rocket rocket)
    {
        _context.Rockets.Update(rocket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRocketAsync(int id)
    {
        var rocket = await _context.Rockets.FindAsync(id);
        if (rocket != null)
        {
            _context.Rockets.Remove(rocket);
            await _context.SaveChangesAsync();
        }
    }
}