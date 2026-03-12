using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.DTOs;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private readonly SpaceProgramContext _context;

    public MissionsController(SpaceProgramContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
    {
    return await _context.Missions
        .Include(m => m.Manager)
        .Include(m => m.Rocket)
        .Include(m => m.LaunchPad)
        .Include(m => m.TargetBody)
        .Include(m => m.Astronauts)
        .Select(m => new MissionDto
        {
            MissionId = m.MissionId,
            Name = m.Name,
            PlannedLaunchDate = m.PlannedLaunchDate,
            PlannedDuration = m.PlannedDuration,
            Status = m.Status.ToString(),
            Type = m.Type.ToString(),
            
            ManagerName = m.Manager.Name != null ? m.Manager.Name : "No Manager",
            RocketModel = m.Rocket.ModelName != null ? m.Rocket.ModelName : "No Rocket",
            LaunchPadLocation = m.LaunchPad.Location != null ? m.LaunchPad.Location : "No Launchpad",
            TargetBodyName = m.TargetBody.Name != null ? m.TargetBody.Name : "No Target",
            
            Crew = m.Astronauts.Select(a => new AstronautDto
            {
                EmployeeId = a.EmployeeId,
                Name = a.Name,
                Rank = a.Rank,
                Paygrade = a.Paygrade,
                HoursInSpace = a.HoursInSpace,
                HoursInSimulation = a.HoursInSimulation
            }).ToList()
        })
        .ToListAsync();
    }
}