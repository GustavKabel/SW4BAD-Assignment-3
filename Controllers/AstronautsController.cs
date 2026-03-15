using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;

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
}