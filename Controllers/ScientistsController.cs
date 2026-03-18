using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScientistsController : ControllerBase
{
    private readonly IScientistRepository _repository;

    public ScientistsController(IScientistRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScientistDto>>> GetScientists()
    {
        var scientists = await _repository.GetAllScientistsAsync();

        var scientistDtos = scientists.Select(s => new ScientistDto
        {
            EmployeeId = s.EmployeeId,
            Name = s.Name,
            Title = s.Title,
            Speciality = s.Speciality
        }).ToList();

        return Ok(scientistDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScientistDto>> GetScientistById(int id)
    {
        var scientist = await _repository.GetScientistByIdAsync(id);
        if (scientist == null) return NotFound($"Scientst with ID {id} was not found.");

        return Ok(new ScientistDto
        {
            EmployeeId = scientist.EmployeeId,
            Name = scientist.Name,
            Title = scientist.Title,
            Speciality = scientist.Speciality
        });
    }

    [HttpPost]
    public async Task<ActionResult<ScientistDto>> CreateScientist(CreateScientistDto dto)
    {
        var scientist = new Scientist
        {
            Name = dto.Name,
            Title = dto.Title,
            Speciality = dto.Speciality
        };

        var created = await _repository.CreateScientistAsync(scientist);

        var returnDto = new ScientistDto
        {
            EmployeeId = created.EmployeeId,
            Name = created.Name,
            Title = created.Title,
            Speciality = created.Speciality
        };

        return Created($"/api/Scientists/{returnDto.EmployeeId}", returnDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScientist(int id, UpdateScientistDto dto)
    {
        var existing = await _repository.GetScientistByIdAsync(id);
        if (existing == null) return NotFound($"Scientist with ID {id} was not found.");

        existing.Name = dto.Name;
        existing.Title = dto.Title;
        existing.Speciality = dto.Speciality;

        await _repository.UpdateScientistAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScientist(int id)
    {
        var existing = await _repository.GetScientistByIdAsync(id);
        if (existing == null) return NotFound($"Scientist with ID {id} was not found.");

        await _repository.DeleteScientistAsync(id);
        return NoContent();
    }
}