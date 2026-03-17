using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class AstronautRepository : IAstronautRepository
{
    private readonly SpaceProgramContext _context;

    public AstronautRepository(SpaceProgramContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Astronaut>> GetAstronautsByExperienceAsync()
    {
        
        return await _context.Astronauts
            .OrderByDescending(a => a.HoursInSpace)
            .ToListAsync();
    }

    public async Task<Astronaut> CreateAstronautAsync(Astronaut astronaut)
    {
        _context.Astronauts.Add(astronaut);
        
        await _context.SaveChangesAsync();
        
        return astronaut;
    }
}