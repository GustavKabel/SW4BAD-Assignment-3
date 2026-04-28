using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Data;

public class SpaceProgramContext : IdentityDbContext<AppUser>
{
    public SpaceProgramContext(DbContextOptions<SpaceProgramContext> options) : base(options) { }
    
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Astronaut> Astronauts { get; set; } = null!;
    public DbSet<Scientist> Scientists { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    public DbSet<Mission> Missions { get; set; } = null!;
    public DbSet<Rocket> Rockets { get; set; } = null!;
    public DbSet<LaunchPad> LaunchPads { get; set; } = null!;
    public DbSet<CelestialBody> CelestialBodies { get; set; } = null!;
    public DbSet<Experiment> Experiments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure Inheritance of Employee entities
        modelBuilder.Entity<Employee>().UseTptMappingStrategy();

        // Many-to-Many: Mission <-> Astronauts
        modelBuilder.Entity<Mission>()
            .HasMany(m => m.Astronauts)
            .WithMany(a => a.Missions)
            .UsingEntity<Dictionary<string, object>>(
                "Mission_Astronaut",
                j => j.HasOne<Astronaut>().WithMany().HasForeignKey("AstronautEmployeeId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<Mission>().WithMany().HasForeignKey("MissionId").OnDelete(DeleteBehavior.NoAction)
            );

        // Many-to-Many: Mission <-> Scientists
        modelBuilder.Entity<Mission>()
            .HasMany(m => m.Scientists)
            .WithMany(s => s.Missions)
            .UsingEntity<Dictionary<string, object>>(
                "Mission_Scientist",
                j => j.HasOne<Scientist>().WithMany().HasForeignKey("ScientistEmployeeId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<Mission>().WithMany().HasForeignKey("MissionId").OnDelete(DeleteBehavior.NoAction)
            );

        // 1-to-1 relation between Mission and Rocket
        modelBuilder.Entity<Mission>()
            .HasOne(m => m.Rocket)
            .WithOne(r => r.Mission)
            .HasForeignKey<Mission>(m => m.RocketId);


        // Ensure rocket weight cannot be negative
        modelBuilder.Entity<Rocket>()
            .ToTable(t => t.HasCheckConstraint("CK_Rocket_Weight", "weight >= 0 "));

        // Launchpads cannot handle more than one mission a day
        modelBuilder.Entity<Mission>()
            .HasIndex(m => new { m.LaunchPadId, m.PlannedLaunchDate })
            .IsUnique();
    }
}