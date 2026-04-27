using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class ExperimentRepository : IExperimentRepository
{
    private readonly SpaceProgramContext _context;
    public ExperimentRepository(SpaceProgramContext context) 
    {
         _context = context; 
    }

    public async Task<IEnumerable<Experiment>> GetAllAsync()
    {
        return await _context.Experiments.ToListAsync();
    }
    
    public async Task<Experiment?> GetByIdAsync(int id)
    {
        return await _context.Experiments.FindAsync(id);
    }
    
    public async Task<Experiment> CreateAsync(Experiment experiment)
    {
        _context.Experiments.Add(experiment);
        await _context.SaveChangesAsync();
        return experiment;
    }

    public async Task UpdateAsync(Experiment experiment)
    {
        _context.Entry(experiment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var exp = await _context.Experiments.FindAsync(id);
        if (exp != null)
        {
            _context.Experiments.Remove(exp);
            await _context.SaveChangesAsync();
        }
    }
}