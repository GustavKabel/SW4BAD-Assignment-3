using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class UpdateAstronautDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Rank { get; set; } = string.Empty;

    [Required]
    public string Paygrade { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Hours in space cannot be negative.")]
    public int HoursInSpace { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Hours in simulation cannot be negative.")]
    public int HoursInSimulation { get; set; }
}