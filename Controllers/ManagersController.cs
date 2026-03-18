using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManagersController : ControllerBase
{
    private readonly IManagerRepository _repository;

    public ManagersController(IManagerRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<ManagerDto>> CreateManager(CreateManagerDto dto)
    {
        var manager = new Manager
        {
            Name = dto.Name,
            Department = dto.Department
        };

        var createdManager = await _repository.CreateManagerAsync(manager);

        var returnDto = new ManagerDto
        {
            EmployeeId = createdManager.EmployeeId,
            Name = createdManager.Name,
            Department = createdManager.Department
        };

        return Created($"/api/Managers/{returnDto.EmployeeId}", returnDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManagerDto>> GetManagerById(int id)
    {
        var manager = await _repository.GetManagerByIdAsync(id);

        if (manager == null)
            return NotFound($"Manager with ID {id} was not found.");

        return Ok(new ManagerDto
        {
            EmployeeId = manager.EmployeeId,
            Name = manager.Name,
            Department = manager.Department
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateManager(int id, UpdateManagerDto dto)
    {
        var existingManager = await _repository.GetManagerByIdAsync(id);
        if (existingManager == null)
        {
            return NotFound($"Manager with ID {id} was not found.");
        }

        existingManager.Name = dto.Name;
        existingManager.Department = dto.Department;

        await _repository.UpdateManagerAsync(existingManager);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManager(int id)
    {
        var existingManager = await _repository.GetManagerByIdAsync(id);
        if (existingManager == null)
        {
            return NotFound($"Manager with id {id} was not found");
        }
        await _repository.DeleteManagerAsync(id);
        return NoContent();
    }
}