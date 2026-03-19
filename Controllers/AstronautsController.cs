using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronautsController : ControllerBase
{
    private readonly IAstronautRepository _repository;

    public AstronautsController(IAstronautRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("experience")]
    public async Task<ActionResult<IEnumerable<AstronautDto>>> GetAstronautExperience()
    {
        var astronauts = await _repository.GetAstronautsByExperienceAsync();

        var astronautDtos = astronauts.Select(a => new AstronautDto
        {
            EmployeeId = a.EmployeeId,
            Name = a.Name,
            Rank = a.Rank,
            Paygrade = a.Paygrade,
            HoursInSpace = a.HoursInSpace,
            HoursInSimulation = a.HoursInSimulation
        }).ToList();

        return Ok(astronautDtos);
    }

    [HttpPost]
    public async Task<ActionResult<AstronautDto>> CreateAstronaut(CreateAstronautDto dto)
    {
        var astronaut = new Astronaut
        {
            Name = dto.Name,
            Rank = dto.Rank,
            Paygrade = dto.Paygrade,
            HoursInSpace = dto.HoursInSpace,
            HoursInSimulation = dto.HoursInSimulation
        };

        var createdAstronaut = await _repository.CreateAstronautAsync(astronaut);

        var returnDto = new AstronautDto
        {
            EmployeeId = createdAstronaut.EmployeeId,
            Name = createdAstronaut.Name,
            Rank = createdAstronaut.Rank,
            Paygrade = createdAstronaut.Paygrade,
            HoursInSpace = createdAstronaut.HoursInSpace,
            HoursInSimulation = createdAstronaut.HoursInSimulation
        };

        return Created($"/api/Astronauts/{returnDto.EmployeeId}", returnDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AstronautDto>> GetAstronautById(int id)
    {
        var astronaut = await _repository.GetAstronautByIdAsync(id);

        if (astronaut == null)
            return NotFound($"Astronaut with ID {id} was not found.");

        return Ok(new AstronautDto
        {
            EmployeeId = astronaut.EmployeeId,
            Name = astronaut.Name,
            Rank = astronaut.Rank,
            Paygrade = astronaut.Paygrade,
            HoursInSpace = astronaut.HoursInSpace,
            HoursInSimulation = astronaut.HoursInSimulation
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAstronaut(int id, UpdateAstronautDto dto)
    {
        var existingAstronaut = await _repository.GetAstronautByIdAsync(id);
        if (existingAstronaut == null)
        {
            return NotFound($"Astronaut with ID {id} was not found.");
        }

        existingAstronaut.Name = dto.Name;
        existingAstronaut.Rank = dto.Rank;
        existingAstronaut.Paygrade = dto.Paygrade;
        existingAstronaut.HoursInSpace = dto.HoursInSpace;
        existingAstronaut.HoursInSimulation = dto.HoursInSimulation;

        await _repository.UpdateAstronautAsync(existingAstronaut);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAstronaut(int id)
    {
        var existingAstronaut = await _repository.GetAstronautByIdAsync(id);
        if (existingAstronaut == null)
        {
            return NotFound($"Astronaut with id {id} was not found");
        }
        await _repository.DeleteAstronautAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AstronautDto>>> GetAstronauts()
    {
        var astronauts = await _repository.GetAllAstronautsAsync();
        var dtos = astronauts.Select(a => new AstronautDto
        {
            EmployeeId = a.EmployeeId,
            Name = a.Name,
            Rank = a.Rank,
            Paygrade = a.Paygrade,
            HoursInSpace = a.HoursInSpace,
            HoursInSimulation = a.HoursInSimulation
        }).ToList();

        return Ok(dtos);
    }
}