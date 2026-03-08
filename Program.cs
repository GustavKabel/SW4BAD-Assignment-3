using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
var app = builder.Build();
// Enable OpenAPI + Scalar
app.MapOpenApi("/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Aarhus Space Program API";
});

// Minimal example endpoint
app.MapGet("/api/astronauts", () =>
{
    return new[]
    {
 new Astronaut(
 "Neil Legstrong",
 "Commander",
 320,
 1200,
 "A3",
 new DateTime(2018, 4, 1)
 ),
 new Astronaut(
 "Mogens Andreassen",
 "Pilot",
 120,
 600,
 "A2",
 new DateTime(2020, 9, 15)
 )
 };
});
app.Run();
record Astronaut(
 string Name,
 string Rank,
 int HoursInSpace,
 int HoursInSimulation,
 string Paygrade,
 DateTime HireDate
);

public class SpaceProgramContext : DbContext
{
    public SpaceProgramContext(DbContextOptions options) : base(options) { }
}

public class Employee
{
    [key]
    public int Employee_id { get; set; }
    public string Name { get; set; }
    public DateOnly Hire_date { get; set; }
}
public class Astronaut : Employee
{
    /*paygrade*/
    public int Paygrade { get; set; }
    /*rank*/
    public string Rank { get; set; }
    /*Experience*/
    public int Hours_in_space { get; set; }
    public int Hours_in_simulation { get; set; }
}

public class Scientist
{
    public string Title { get; set; }
    public string Speciality { get; set; }
}

public class Manager
{
    public string Department { get; set; }
}
