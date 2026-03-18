using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class UpdateManagerDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Department { get; set; } = string.Empty;

}