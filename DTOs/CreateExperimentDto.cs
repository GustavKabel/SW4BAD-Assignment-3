using System.ComponentModel.DataAnnotations;
namespace AarhusSpaceProgram.Api.DTOs;
public class CreateExperimentDto
{
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    [Required] public int MissionId { get; set; }
    [Required] public int ScientistId { get; set; }
}