using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class ScientistRepository : IScientistRepository
{
    private readonly SpaceProgramContext _context;

    public ScientistRepository(SpaceProgramContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Scientist>> GetAllScientistsAsync()
    {
        return await _context.Scientists.ToListAsync();
    }
}