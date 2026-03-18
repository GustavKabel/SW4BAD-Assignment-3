using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Repositories;
using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        var employees = await _repository.GetAllEmployeesAsync();
        var dtos = employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            Name = e.Name,
            HireDate = e.HireDate
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var employee = await _repository.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound($"Employee with ID {id} not found.");

        return Ok(new EmployeeDto
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            HireDate = employee.HireDate
        });
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto dto)
    {
        var employee = new Employee 
        { 
            Name = dto.Name,
            HireDate = dto.HireDate 
        };
        
        var created = await _repository.AddEmployeeAsync(employee);

        var returnDto = new EmployeeDto
        {
            EmployeeId = created.EmployeeId,
            Name = created.Name,
            HireDate = created.HireDate
        };

        return CreatedAtAction(nameof(GetEmployeeById), new { id = returnDto.EmployeeId }, returnDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto dto)
    {
        var existing = await _repository.GetEmployeeByIdAsync(id);
        if (existing == null) return NotFound($"Employee with ID {id} not found.");

        existing.Name = dto.Name;
        existing.HireDate = dto.HireDate;
        
        await _repository.UpdateEmployeeAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var existing = await _repository.GetEmployeeByIdAsync(id);
        if (existing == null) return NotFound($"Employee with ID {id} not found.");

        await _repository.DeleteEmployeeAsync(id);
        return NoContent();
    }
}