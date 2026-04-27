using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class CreateRocketDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Model name cannot exceed 100 characters")]
    public string ModelName { get; set; } = string.Empty;

    public int PayloadCap { get; set; }
    public int CrewCap { get; set; }
    public int NoOfStages { get; set; }
    public int FuelCap { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Rocket weight cannot be negative.")]
    public double Weight { get; set; }
}