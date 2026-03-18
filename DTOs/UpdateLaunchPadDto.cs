using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class UpdateLaunchPadDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    public string Status { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Max supported weight cannot be negative.")]
    public double MaxWeight { get; set; }
}