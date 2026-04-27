using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Entities;
using AarhusSpaceProgram.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AarhusSpaceProgram.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExperimentsController : ControllerBase
{
    private readonly IExperimentRepository _repository;

    public ExperimentsController(IExperimentRepository repository)
    {
        _repository = repository;
    }

    [Authorize(Policy = "CanViewExperiments")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperimentDto>>> GetExperiments()
    {
        var experiments = await _repository.GetAllAsync();
        var dtos = experiments.Select(e => new ExperimentDto
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            CreationDate = e.CreationDate,
            MissionId = e.MissionId,
            ScientistId = e.ScientistId
        }).ToList();
        
        return Ok(dtos);
    }

    [Authorize(Policy = "CanViewExperiments")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ExperimentDto>> GetExperiment(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return NotFound($"Experiment with ID {id} was not found.");
        return Ok(new ExperimentDto 
        { 
            Id = e.Id, 
            Name = e.Name, 
            Description = e.Description, 
            CreationDate = e.CreationDate, 
            MissionId = e.MissionId, 
            ScientistId = e.ScientistId 
        });
    }

    [Authorize(Policy = "CanModifyExperiments")]
    [HttpPost]
    public async Task<ActionResult<ExperimentDto>> CreateExperiment(CreateExperimentDto dto)
    {
        var exp = new Experiment 
        { 
            Name = dto.Name!, 
            Description = dto.Description!, 
            MissionId = dto.MissionId, 
            ScientistId = dto.ScientistId 
        };

        var createdExperiment = await _repository.CreateAsync(exp);

        var returnDto = new ExperimentDto
        {
            Id = createdExperiment.Id,
            Name = createdExperiment.Name,
            Description = createdExperiment.Description,
            CreationDate = createdExperiment.CreationDate,
            MissionId = createdExperiment.MissionId,
            ScientistId = createdExperiment.ScientistId
        };

        return Created($"/api/Experiment/{returnDto.Id}", returnDto);

    }

    [Authorize(Policy = "CanModifyExperiments")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExperiment(int id, UpdateExperimentDto dto)
    {
        var exp = await _repository.GetByIdAsync(id);
        if (exp == null) return NotFound($"Experiment with ID {id} was not found.");

        exp.Name = dto.Name!;
        exp.Description = dto.Description!;
        await _repository.UpdateAsync(exp);
        return NoContent();
    }

    [Authorize(Policy = "CanModifyExperiments")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperiment(int id)
    {
        var exp = await _repository.GetByIdAsync(id);

        if (exp == null) 
        { 
            return NotFound($"Experiment with ID {id} was not found."); 
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}