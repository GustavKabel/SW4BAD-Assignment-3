using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RocketsController : ControllerBase
{
    private readonly IRocketRepository _repository;

    public RocketsController(IRocketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RocketDto>>> GetRockets()
    {
        var rockets = await _repository.GetAllRocketsAsync();
        var dtos = rockets.Select(r => new RocketDto
        {
            RocketId = r.RocketId,
            ModelName = r.ModelName,
            PayloadCap = r.PayloadCap,
            CrewCap = r.CrewCap,
            NoOfStages = r.NoOfStages,
            FuelCap = r.FuelCap,
            Weight = r.Weight
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RocketDto>> GetRocketById(int id)
    {
        var rocket = await _repository.GetRocketByIdAsync(id);
        if (rocket == null) return NotFound($"Rocket with ID {id} not found.");

        return Ok(new RocketDto
        {
            RocketId = rocket.RocketId,
            ModelName = rocket.ModelName,
            PayloadCap = rocket.PayloadCap,
            CrewCap = rocket.CrewCap,
            NoOfStages = rocket.NoOfStages,
            FuelCap = rocket.FuelCap,
            Weight = rocket.Weight
        });
    }

    [HttpPost]
    public async Task<ActionResult<RocketDto>> CreateRocket(CreateRocketDto dto)
    {
        var rocket = new Rocket
        {
            ModelName = dto.ModelName,
            PayloadCap = dto.PayloadCap,
            CrewCap = dto.CrewCap,
            NoOfStages = dto.NoOfStages,
            FuelCap = dto.FuelCap,
            Weight = dto.Weight
        };

        var created = await _repository.AddRocketAsync(rocket);

        var returnDto = new RocketDto
        {
            RocketId = created.RocketId,
            ModelName = created.ModelName,
            PayloadCap = created.PayloadCap,
            CrewCap = created.CrewCap,
            NoOfStages = created.NoOfStages,
            FuelCap = created.FuelCap,
            Weight = created.Weight
        };

        return CreatedAtAction(nameof(GetRocketById), new { id = returnDto.RocketId }, returnDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRocket(int id, UpdateRocketDto dto)
    {
        var existing = await _repository.GetRocketByIdAsync(id);
        if (existing == null) return NotFound($"Rocket with ID {id} not found.");

        existing.ModelName = dto.ModelName;
        existing.PayloadCap = dto.PayloadCap;
        existing.CrewCap = dto.CrewCap;
        existing.NoOfStages = dto.NoOfStages;
        existing.FuelCap = dto.FuelCap;
        existing.Weight = dto.Weight;

        await _repository.UpdateRocketAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRocket(int id)
    {
        var existing = await _repository.GetRocketByIdAsync(id);
        if (existing == null) return NotFound($"Rocket with ID {id} not found.");

        await _repository.DeleteRocketAsync(id);
        return NoContent();
    }
}