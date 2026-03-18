using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class UpdateEmployeeDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateOnly HireDate { get; set; }
}