using AarhusSpaceProgram.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Data;



public class SpaceProgramContext : DbContext
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

        // Seeding the database

        //Managers
        modelBuilder.Entity<Manager>().HasData(
            new Manager { EmployeeId = 1, Name = "Dumbledore", HireDate = new DateOnly(2001, 1, 1), Department = "Flight control" },
            new Manager { EmployeeId = 2, Name = "Voldemord", HireDate = new DateOnly(2002, 1, 1), Department = "Engineering" }
        );

        //Scientists
        modelBuilder.Entity<Scientist>().HasData(
            new Scientist { EmployeeId = 3, Name = "Hermoine Granger", HireDate = new DateOnly(2004, 2, 5), Title = "Astronomer", Speciality = "Mars" },
            new Scientist { EmployeeId = 4, Name = "Harry Potter", HireDate = new DateOnly(2004, 1, 1), Title = "Alien investigator", Speciality = "Alien life" }
        );

        //Astronauts
        modelBuilder.Entity<Astronaut>().HasData(
            new Astronaut { EmployeeId = 5, Name = "Ron Weasley", HireDate = new DateOnly(2002, 1, 1), Paygrade = "0-8", Rank = "Commander", HoursInSpace = 200, HoursInSimulation = 600 },
            new Astronaut { EmployeeId = 6, Name = "Neville Longbottom", HireDate = new DateOnly(2003, 1, 1), Paygrade = "0-7", Rank = "Pilot", HoursInSpace = 150, HoursInSimulation = 500 },
            new Astronaut { EmployeeId = 7, Name = "Draco Malfoy", HireDate = new DateOnly(2004, 1, 1), Paygrade = "0-7", Rank = "Pilot", HoursInSpace = 100, HoursInSimulation = 550 }
        );

        //Rockets
        modelBuilder.Entity<Rocket>().HasData(
            new Rocket { RocketId = 1, ModelName = "Superfly", PayloadCap = 14000, CrewCap = 3, NoOfStages = 3, FuelCap = 200000, Weight = 296000 },
            new Rocket { RocketId = 2, ModelName = "BC Rocket", PayloadCap = 13000, CrewCap = 2, NoOfStages = 2, FuelCap = 150000, Weight = 230000 }
        );

        //LaunchPads
        modelBuilder.Entity<LaunchPad>().HasData(
            new LaunchPad { LaunchPadId = 1, Location = "Hogwartz", Status = "Active", MaxWeight = 3000000 },
            new LaunchPad { LaunchPadId = 2, Location = "The forbidden forest", Status = "Active", MaxWeight = 4000000 }
        );

        //Celestial Bodies
        modelBuilder.Entity<CelestialBody>().HasData(
            new CelestialBody { BodyId = 1, Name = "Earth", Distance = 0, BodyType = CelestialBodyType.Planet, Subtype = CelestialBodySubType.RockyPlanet, ParentBodyId = null },
            new CelestialBody { BodyId = 2, Name = "Moon", Distance = 384400, BodyType = CelestialBodyType.Moon, Subtype = CelestialBodySubType.None, ParentBodyId = 1 },
            new CelestialBody { BodyId = 3, Name = "Mars", Distance = 225000000, BodyType = CelestialBodyType.Planet, Subtype = CelestialBodySubType.RockyPlanet, ParentBodyId = null }
        );

        //Missions
        modelBuilder.Entity<Mission>().HasData(
            new Mission { MissionId = 1, Name = "Apollo 11", PlannedLaunchDate = new DateOnly(2010, 7, 16), PlannedDuration = 8, Status = MissionStatus.Completed, Type = TypeOfMission.Landing, ManagerId = 1, RocketId = 1, LaunchPadId = 1, TargetBodyId = 2 },
            new Mission { MissionId = 2, Name = "Artemis I", PlannedLaunchDate = new DateOnly(2025, 11, 16), PlannedDuration = 25, Status = MissionStatus.Active, Type = TypeOfMission.Orbiting, ManagerId = 2, RocketId = 2, LaunchPadId = 2, TargetBodyId = 2 }
        );

        // Seeding the Many-to-Many Join Tables
        // EF Core creates invisible shadow properties for join tables based on the class names
        modelBuilder.Entity("Mission_Astronaut").HasData(
            new { AstronautsEmployeeId = 5, MissionsMissionId = 1 },
            new { AstronautsEmployeeId = 6, MissionsMissionId = 1 },
            new { AstronautsEmployeeId = 7, MissionsMissionId = 1 },
            new { AstronautsEmployeeId = 6, MissionsMissionId = 2 },
            new { AstronautsEmployeeId = 7, MissionsMissionId = 2 }
        );
    }
}



