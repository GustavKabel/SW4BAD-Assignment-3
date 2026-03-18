using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly SpaceProgramContext _context;

    public ManagerRepository(SpaceProgramContext context)
    {
        _context = context;
    }

    public async Task<Manager> CreateManagerAsync(Manager manager)
    {
        _context.Managers.Add(manager);

        await _context.SaveChangesAsync();

        return manager;
    }

    public async Task<Manager?> GetManagerByIdAsync(int id)
    {
        return await _context.Managers.FindAsync(id);
    }

    public async Task UpdateManagerAsync(Manager manager)
    {
        _context.Managers.Update(manager);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteManagerAsync(int id)
    {
        var manager = await _context.Managers.FindAsync(id);

        if (manager != null)
        {
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
        }
    }

}