using AarhusSpaceProgram.Api.entities;

using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.data;



public class SpaceProgramContext : DbContext
{
    public SpaceProgramContext(DbContextOptions options) : base(options) { }
    public DbSet<Employee> Employees {get; set; } = null!;
}



