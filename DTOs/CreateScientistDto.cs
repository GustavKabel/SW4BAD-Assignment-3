using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class CreateScientistDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Speciality { get; set; } = string.Empty;
}