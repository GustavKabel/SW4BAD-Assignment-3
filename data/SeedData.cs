using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AarhusSpaceProgram.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SpaceProgramContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // 1. SEED ROLES
        string[] roles = { "Manager", "Scientist", "Astronaut" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Helper function to create a User and return its generated string ID
        async Task<string> CreateUserAsync(string name, string role)
        {
            var email = $"{name.Replace(" ", "").ToLower()}@space.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser { UserName = email, Email = email, FullName = name };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, role);
            }
            return user.Id;
        }

        // 2. SEED EMPLOYEES & LINK TO IDENTITY
        if (!context.Employees.Any())
        {
            // Note: No more EmployeeId! We let SQL Server auto-generate them.
            context.Managers.AddRange(
                new Manager { Name = "Dumbledore", HireDate = new DateOnly(2001, 1, 1), Department = "Flight control", AppUserId = await CreateUserAsync("Dumbledore", "Manager") },
                new Manager { Name = "Voldemord", HireDate = new DateOnly(2002, 1, 1), Department = "Engineering", AppUserId = await CreateUserAsync("Voldemord", "Manager") }
            );

            context.Scientists.AddRange(
                new Scientist { Name = "Hermoine Granger", HireDate = new DateOnly(2004, 2, 5), Title = "Astronomer", Speciality = "Mars", AppUserId = await CreateUserAsync("Hermoine Granger", "Scientist") },
                new Scientist { Name = "Harry Potter", HireDate = new DateOnly(2004, 1, 1), Title = "Alien investigator", Speciality = "Alien life", AppUserId = await CreateUserAsync("Harry Potter", "Scientist") }
            );

            context.Astronauts.AddRange(
                new Astronaut { Name = "Ron Weasley", HireDate = new DateOnly(2002, 1, 1), Paygrade = "0-8", Rank = "Commander", HoursInSpace = 200, HoursInSimulation = 600, AppUserId = await CreateUserAsync("Ron Weasley", "Astronaut") },
                new Astronaut { Name = "Neville Longbottom", HireDate = new DateOnly(2003, 1, 1), Paygrade = "0-7", Rank = "Pilot", HoursInSpace = 150, HoursInSimulation = 500, AppUserId = await CreateUserAsync("Neville Longbottom", "Astronaut") },
                new Astronaut { Name = "Draco Malfoy", HireDate = new DateOnly(2004, 1, 1), Paygrade = "0-7", Rank = "Pilot", HoursInSpace = 100, HoursInSimulation = 550, AppUserId = await CreateUserAsync("Draco Malfoy", "Astronaut") }
            );

            await context.SaveChangesAsync();
        }

        // 3. SEED BASE DATA (Rockets, Pads, Bodies)
        if (!context.Rockets.Any())
        {
            context.Rockets.AddRange(
                new Rocket { ModelName = "Superfly", PayloadCap = 14000, CrewCap = 3, NoOfStages = 3, FuelCap = 200000, Weight = 296000.0 },
                new Rocket { ModelName = "BC Rocket", PayloadCap = 13000, CrewCap = 2, NoOfStages = 2, FuelCap = 150000, Weight = 230000.0 }
            );
            context.LaunchPads.AddRange(
                new LaunchPad { Location = "Hogwartz", Status = "Active", MaxWeight = 3000000.0 },
                new LaunchPad { Location = "The forbidden forest", Status = "Active", MaxWeight = 4000000.0 }
            );
            
            // For Celestial Bodies, we link the Moon to Earth via the object property!
            var earth = new CelestialBody { Name = "Earth", Distance = 0, BodyType = CelestialBodyType.Planet, Subtype = CelestialBodySubType.RockyPlanet, ParentBodyId = null };
            var moon = new CelestialBody { Name = "Moon", Distance = 384400, BodyType = CelestialBodyType.Moon, Subtype = CelestialBodySubType.None, ParentBody = earth };
            var mars = new CelestialBody { Name = "Mars", Distance = 225000000, BodyType = CelestialBodyType.Planet, Subtype = CelestialBodySubType.RockyPlanet, ParentBodyId = null };
            context.CelestialBodies.AddRange(earth, moon, mars);
            
            await context.SaveChangesAsync();
        }

        // 4. SEED MISSIONS & MANY-TO-MANY RELATIONSHIPS
        if (!context.Missions.Any())
        {
            // Instead of guessing IDs, we fetch exactly who we need by name!
            var ron = await context.Astronauts.FirstOrDefaultAsync(a => a.Name == "Ron Weasley");
            var neville = await context.Astronauts.FirstOrDefaultAsync(a => a.Name == "Neville Longbottom");
            var draco = await context.Astronauts.FirstOrDefaultAsync(a => a.Name == "Draco Malfoy");
            var hermione = await context.Scientists.FirstOrDefaultAsync(s => s.Name == "Hermoine Granger");
            var harry = await context.Scientists.FirstOrDefaultAsync(s => s.Name == "Harry Potter");
            
            var dumbledore = await context.Managers.FirstOrDefaultAsync(m => m.Name == "Dumbledore");
            var voldemort = await context.Managers.FirstOrDefaultAsync(m => m.Name == "Voldemord");
            var superfly = await context.Rockets.FirstOrDefaultAsync(r => r.ModelName == "Superfly");
            var bcRocket = await context.Rockets.FirstOrDefaultAsync(r => r.ModelName == "BC Rocket");
            var hogwarts = await context.LaunchPads.FirstOrDefaultAsync(l => l.Location == "Hogwartz");
            var forest = await context.LaunchPads.FirstOrDefaultAsync(l => l.Location == "The forbidden forest");
            var earth = await context.CelestialBodies.FirstOrDefaultAsync(b => b.Name == "Earth");
            var moon = await context.CelestialBodies.FirstOrDefaultAsync(b => b.Name == "Moon");

            var apollo11 = new Mission
            {
                Name = "Apollo 11", PlannedLaunchDate = new DateOnly(2010, 7, 16), PlannedDuration = 8, Status = MissionStatus.Completed, Type = TypeOfMission.Landing,
                Manager = dumbledore, Rocket = superfly, LaunchPad = hogwarts, TargetBody = earth
            };
            if(ron != null) apollo11.Astronauts.Add(ron);
            if(neville != null) apollo11.Astronauts.Add(neville);
            if(draco != null) apollo11.Astronauts.Add(draco);
            if(hermione != null) apollo11.Scientists.Add(hermione);
            if(harry != null) apollo11.Scientists.Add(harry);

            var artemis = new Mission
            {
                Name = "Artemis I", PlannedLaunchDate = new DateOnly(2025, 11, 16), PlannedDuration = 25, Status = MissionStatus.Active, Type = TypeOfMission.Orbiting,
                Manager = voldemort, Rocket = bcRocket, LaunchPad = forest, TargetBody = moon
            };
            if(neville != null) artemis.Astronauts.Add(neville);
            if(draco != null) artemis.Astronauts.Add(draco);
            if(hermione != null) artemis.Scientists.Add(hermione);

            context.Missions.AddRange(apollo11, artemis);
            await context.SaveChangesAsync();
            
            // 5. SEED AN EXPERIMENT
            if (!context.Experiments.Any())
            {
                context.Experiments.Add(new Experiment
                {
                    Name = "Magic Plant Growth", Description = "Testing mandrakes in microgravity.",
                    Mission = apollo11, // Link to the object, not the ID!
                    Scientist = hermione // Link to the object, not the ID!
                });
                await context.SaveChangesAsync();
            }
        }
    }
}