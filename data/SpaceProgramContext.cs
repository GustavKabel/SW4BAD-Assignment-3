using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Data;



public class SpaceProgramContext : DbContext
{
    public SpaceProgramContext(DbContextOptions options) : base(options) { }
    public DbSet<Employee> Employees {get; set; } = null!;
    public DbSet<Astronaut> Astronauts { get; set; } = null!;
    public DbSet<Scientist> Scientists { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    public DbSet<Mission> Missions { get; set; } = null!;
    public DbSet<Rocket> Rockets { get; set; } = null!;
    public DbSet<LaunchPad> LaunchPads { get; set; } = null!;
    public DbSet<CelestialBody> CelestialBodies { get; set; } = null!;

    // Fluent API stuff

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This is for inheritance for employees
        modelBuilder.Entity<Employee>().UseTptMappingStrategy();
        
        // Now we make Fluent API for our many-to-many relations
        modelBuilder.Entity<Mission>()
        .HasMany(m => m.Astronauts)
        .WithMany(a => a.Missions)
        .UsingEntity(j => j.ToTable("Mission_Astronaut"));

        modelBuilder.Entity<Mission>()
        .HasMany(m => m.Scientists)
        .WithMany(s => s.Missions)
        .UsingEntity(j => j.ToTable("Mission_Scientist"));
            
        // 1-to-1 relation between Mission and Rocket
        modelBuilder.Entity<Mission>()
        .HasOne(m => m.Rocket)
        .WithOne(r => r.Mission)
        .HasForeignKey<Mission>(m => m.RocketId);

        // Part D. Extra Rules
        // Ensure rocket weight cannot be negative
        modelBuilder.Entity<Rocket>()
        .ToTable(t => t.HasCheckConstraint("CK_Rocket_Weight", "weight >= 0 "));

        // Launchpads cannot handle more than one mission a day
        modelBuilder.Entity<Mission>()
        .HasIndex(m => new { m.LaunchPadId, m.PlannedLaunchDate })
        .IsUnique();
        
    }
}



