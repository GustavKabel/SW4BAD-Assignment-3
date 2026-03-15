using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;

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
}