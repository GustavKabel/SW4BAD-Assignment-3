using System.ComponentModel.DataAnnotations;
namespace AarhusSpaceProgram.Api.DTOs;
public class UpdateExperimentDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
}