using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AarhusSpaceProgram.Api.Data;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    // private readonly SpaceProgramContext _context;
    private readonly IMissionRepository _repository;

    public MissionsController(IMissionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
    {

        var missions = await _repository.GetAllMissionsAsync();

        var missionDtos = missions.Select(m => new MissionDto
        {
            MissionId = m.MissionId,
            Name = m.Name,
            PlannedLaunchDate = m.PlannedLaunchDate,
            PlannedDuration = m.PlannedDuration,
            Status = m.Status.ToString(),
            Type = m.Type.ToString(),

            ManagerName = m.Manager != null ? m.Manager.Name : "No Manager",
            RocketModel = m.Rocket != null ? m.Rocket.ModelName : "No Rocket",
            LaunchPadLocation = m.LaunchPad != null ? m.LaunchPad.Location : "No Launchpad",
            TargetBodyName = m.TargetBody != null ? m.TargetBody.Name : "No Target",

            Crew = m.Astronauts.Select(a => new AstronautDto
            {
                EmployeeId = a.EmployeeId,
                Name = a.Name,
                Rank = a.Rank,
                Paygrade = a.Paygrade,
                HoursInSpace = a.HoursInSpace,
                HoursInSimulation = a.HoursInSimulation
            }).ToList()
        }).ToList();

        return Ok(missionDtos);
    }
}