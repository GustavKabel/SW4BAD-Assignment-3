using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MissionsController : ControllerBase
{
    private readonly IMissionRepository _missionRepo;
    private readonly IRocketRepository _rocketRepo;
    private readonly ILaunchPadRepository _launchPadRepo;

    public MissionsController(
        IMissionRepository missionRepo, 
        IRocketRepository rocketRepo, 
        ILaunchPadRepository launchPadRepo)
    {
        _missionRepo = missionRepo;
        _rocketRepo = rocketRepo;
        _launchPadRepo = launchPadRepo;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
    {
        var missions = await _missionRepo.GetAllMissionsAsync();

        var missionDtos = missions.Select(m => new MissionDto
        {
            MissionId = m.MissionId,
            Name = m.Name,
            PlannedLaunchDate = m.PlannedLaunchDate,
            PlannedDuration = m.PlannedDuration,
            Status = m.Status,
            Type = m.Type,

            ManagerName = m.Manager?.Name ?? "No Manager",
            RocketModel = m.Rocket?.ModelName ?? "No Rocket",
            LaunchPadLocation = m.LaunchPad?.Location ?? "No Launchpad",
            TargetBodyName = m.TargetBody?.Name ?? "No Target",
        }).ToList();

        return Ok(missionDtos);
    }

    [Authorize(Policy = "ReadOnly")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MissionDetailsDto>> GetMissionById(int id)
    {
        var mission = await _missionRepo.GetMissionByIdAsync(id);

        if (mission == null)
        {
            return NotFound($"Mission with ID {id} was not found.");
        }

        var missionDetailsDto = new MissionDetailsDto
        {
            MissionId = mission.MissionId,
            Name = mission.Name,
            PlannedLaunchDate = mission.PlannedLaunchDate,
            PlannedDuration = mission.PlannedDuration,
            Status = mission.Status,
            Type = mission.Type,
            
            ManagerName = mission.Manager != null ? mission.Manager.Name : "No Manager",
            RocketModel = mission.Rocket != null ? mission.Rocket.ModelName : "No Rocket",
            LaunchPadLocation = mission.LaunchPad.Location ?? "No Launchpad",
            TargetBodyName = mission.TargetBody.Name ?? "No Target",
            
            Crew = mission.Astronauts.Select(a => new AstronautDto
            {
                EmployeeId = a.EmployeeId,
                Name = a.Name,
                Rank = a.Rank,
                Paygrade = a.Paygrade,
                HoursInSpace = a.HoursInSpace,
                HoursInSimulation = a.HoursInSimulation
            }).ToList(),

            Scientists = mission.Scientists.Select(s => new ScientistDto
            {
                EmployeeId = s.EmployeeId,
                Name = s.Name,
                Title = s.Title,
                Speciality = s.Speciality
            }).ToList()
        };

        return Ok(missionDetailsDto);
    }

    [Authorize(Policy = "ReadOnly")]
    [HttpGet("target/{bodyName}")]
    public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissionsByTargetBody(string bodyName)
    {
        var missions = await _missionRepo.GetMissionsByTargetBodyAsync(bodyName);

        if (!missions.Any())
        {
            return NotFound($"No missions found targeting '{bodyName}'.");
        }

        var missionDtos = missions.Select(m => new MissionDto
        {
            MissionId = m.MissionId,
            Name = m.Name,
            PlannedLaunchDate = m.PlannedLaunchDate,
            PlannedDuration = m.PlannedDuration,
            Status = m.Status,
            Type = m.Type,
            
            ManagerName = m.Manager != null ? m.Manager.Name : "No Manager",
            RocketModel = m.Rocket != null ? m.Rocket.ModelName : "No Rocket",
            LaunchPadLocation = m.LaunchPad.Location ?? "No Launchpad",
            TargetBodyName = m.TargetBody.Name ?? "No Target",
            
        }).ToList();

        return Ok(missionDtos);
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPost]
    public async Task<ActionResult<MissionDto>> CreateMission(CreateMissionDto dto)
    {
        var rocket = await _rocketRepo.GetRocketByIdAsync(dto.RocketId);
        var launchPad = await _launchPadRepo.GetLaunchPadByIdAsync(dto.LaunchPadId);
        
        if (rocket == null) return BadRequest("The specified rocket does not exist.");
        if (launchPad == null) return BadRequest("The specified launchpad does not exist.");

        if (rocket.Weight > launchPad.MaxWeight)
        {
            return BadRequest($"Rocket weight ({rocket.Weight}) exceeds the launchpad's maximum supported weight ({launchPad.MaxWeight}).");
        }
        var isBooked = await _missionRepo.HasMissionOnDateAsync(dto.LaunchPadId, dto.PlannedLaunchDate);
        if (isBooked)
        {
            return BadRequest("This launchpad is already booked for a mission on this date.");
        }

        var mission = new Mission
        {
            Name = dto.Name,
            PlannedLaunchDate = dto.PlannedLaunchDate,
            PlannedDuration = dto.PlannedDuration,
            Type = dto.Type,
            ManagerId = dto.ManagerId,
            RocketId = dto.RocketId,
            LaunchPadId = dto.LaunchPadId,
            TargetBodyId = dto.TargetBodyId,
            Status = MissionStatus.Created
        };

        var created = await _missionRepo.AddMissionAsync(mission);

        return CreatedAtAction(nameof(GetMissionById), new { id = created.MissionId }, new { id = created.MissionId, message = "Mission successfully created. Please call GET /api/Missions/{id} for full details." });
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMission(int id, UpdateMissionDto dto)
    {
        var existing = await _missionRepo.GetMissionByIdAsync(id);
        if (existing == null) return NotFound($"Mission with ID {id} not found.");


        var rocket = await _rocketRepo.GetRocketByIdAsync(dto.RocketId);
        var launchPad = await _launchPadRepo.GetLaunchPadByIdAsync(dto.LaunchPadId);
        
        if (rocket != null && launchPad != null && rocket.Weight > launchPad.MaxWeight)
        {
            return BadRequest($"Rocket weight ({rocket.Weight}) exceeds the launchpad's maximum supported weight ({launchPad.MaxWeight}).");
        }

        var isBooked = await _missionRepo.HasMissionOnDateAsync(dto.LaunchPadId, dto.PlannedLaunchDate, id);
        if (isBooked)
        {
            return BadRequest("This launchpad is already booked for a different mission on this date.");
        }

        try
        {
            existing.UpdateStatus(dto.Status);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        
        existing.Name = dto.Name;
        existing.PlannedLaunchDate = dto.PlannedLaunchDate;
        existing.PlannedDuration = dto.PlannedDuration;
        existing.Type = dto.Type;
        existing.ManagerId = dto.ManagerId;
        existing.RocketId = dto.RocketId;
        existing.LaunchPadId = dto.LaunchPadId;
        existing.TargetBodyId = dto.TargetBodyId;

        await _missionRepo.UpdateMissionAsync(existing);
        return NoContent();
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMission(int id)
    {
        var existing = await _missionRepo.GetMissionByIdAsync(id);
        if (existing == null) return NotFound($"Mission with ID {id} not found.");

        await _missionRepo.DeleteMissionAsync(id);
        return NoContent();
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPost("{missionId}/astronauts/{astronautId}")]
    public async Task<IActionResult> AssignAstronaut(int missionId, int astronautId)
    {
        var success = await _missionRepo.AssignAstronautAsync(missionId, astronautId);
        
        if (!success) return NotFound("Either the Mission or the Astronaut was not found.");
        
        return Ok(new { message = $"Astronaut {astronautId} was successfully assigned to Mission {missionId}." });
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpDelete("{missionId}/astronauts/{astronautId}")]
    public async Task<IActionResult> RemoveAstronaut(int missionId, int astronautId)
    {
        var success = await _missionRepo.RemoveAstronautAsync(missionId, astronautId);
        
        if (!success) return NotFound("Either the Mission or the Astronaut was not found.");
        
        return Ok(new { message = $"Astronaut {astronautId} was successfully removed from Mission {missionId}." });
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPost("{missionId}/scientists/{scientistId}")]
    public async Task<IActionResult> AssignScientist(int missionId, int scientistId)
    {
        var success = await _missionRepo.AssignScientistAsync(missionId, scientistId);
        
        if (!success) return NotFound("Either the Mission or the Scientist was not found.");
        
        return Ok(new { message = $"Scientist {scientistId} was successfully assigned to Mission {missionId}." });
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpDelete("{missionId}/scientists/{scientistId}")]
    public async Task<IActionResult> RemoveScientist(int missionId, int scientistId)
    {
        var success = await _missionRepo.RemoveScientistAsync(missionId, scientistId);
        
        if (!success) return NotFound("Either the Mission or the Scientist was not found.");
        
        return Ok(new { message = $"Scientist {scientistId} was successfully removed from Mission {missionId}." });
    }
}