using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AarhusSpaceProgram.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LaunchPadsController : ControllerBase
{
    private readonly ILaunchPadRepository _repository;

    public LaunchPadsController(ILaunchPadRepository repository)
    {
        _repository = repository;
    }

    [Authorize(Policy = "ReadOnly")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LaunchPadDto>>> GetLaunchPads()
    {
        var launchPads = await _repository.GetAllLaunchPadsAsync();
        var dtos = launchPads.Select(lp => new LaunchPadDto
        {
            LaunchPadId = lp.LaunchPadId,
            Location = lp.Location,
            Status = lp.Status,
            MaxWeight = lp.MaxWeight
        }).ToList();

        return Ok(dtos);
    }

    [Authorize(Policy = "ReadOnly")]
    [HttpGet("{id}")]
    public async Task<ActionResult<LaunchPadDto>> GetLaunchPadById(int id)
    {
        var launchPad = await _repository.GetLaunchPadByIdAsync(id);
        if (launchPad == null) return NotFound($"Launchpad with ID {id} not found.");

        return Ok(new LaunchPadDto
        {
            LaunchPadId = launchPad.LaunchPadId,
            Location = launchPad.Location,
            Status = launchPad.Status,
            MaxWeight = launchPad.MaxWeight
        });
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPost]
    public async Task<ActionResult<LaunchPadDto>> CreateLaunchPad(CreateLaunchPadDto dto)
    {
        var launchPad = new LaunchPad
        {
            Location = dto.Location,
            Status = dto.Status,
            MaxWeight = dto.MaxWeight
        };

        var created = await _repository.AddLaunchPadAsync(launchPad);

        var returnDto = new LaunchPadDto
        {
            LaunchPadId = created.LaunchPadId,
            Location = created.Location,
            Status = created.Status,
            MaxWeight = created.MaxWeight
        };

        return CreatedAtAction(nameof(GetLaunchPadById), new { id = returnDto.LaunchPadId }, returnDto);
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLaunchPad(int id, UpdateLaunchPadDto dto)
    {
        var existing = await _repository.GetLaunchPadByIdAsync(id);
        if (existing == null) return NotFound($"Launchpad with ID {id} not found.");

        existing.Location = dto.Location;
        existing.Status = dto.Status;
        existing.MaxWeight = dto.MaxWeight;

        await _repository.UpdateLaunchPadAsync(existing);

        return NoContent();
    }

    [Authorize(Policy = "ManagerFullAccess")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLaunchPad(int id)
    {
        var existing = await _repository.GetLaunchPadByIdAsync(id);
        if (existing == null) return NotFound($"Launchpad with ID {id} not found.");

        await _repository.DeleteLaunchPadAsync(id);
        return NoContent();
    }
}