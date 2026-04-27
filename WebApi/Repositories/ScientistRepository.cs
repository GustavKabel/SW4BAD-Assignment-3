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

    public async Task<Scientist> CreateScientistAsync(Scientist scientist)
    {
        _context.Scientists.Add(scientist);
        await _context.SaveChangesAsync();
        return scientist;
    }

    public async Task DeleteScientistAsync(int id)
    {
        var scientist = await _context.Scientists.FindAsync(id);
        if (scientist != null)
        {
            _context.Scientists.Remove(scientist);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Scientist>> GetAllScientistsAsync()
    {
        return await _context.Scientists.ToListAsync();
    }

    public async Task<Scientist?> GetScientistByIdAsync(int id)
    {
        return await _context.Scientists.FindAsync(id);
    }

    public async Task UpdateScientistAsync(Scientist scientist)
    {
        _context.Scientists.Update(scientist);
        await _context.SaveChangesAsync();
    }
}