using System.ComponentModel.DataAnnotations;
using AarhusSpaceProgram.Api.Entities;

namespace AarhusSpaceProgram.Api.DTOs;

public class UpdateMissionDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Mission name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public DateOnly PlannedLaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    
    [Required]
    public TypeOfMission Type { get; set; }
    
    [Required]
    public MissionStatus Status { get; set; }

    public int ManagerId { get; set; }
    public int RocketId { get; set; }
    public int LaunchPadId { get; set; }
    public int TargetBodyId { get; set; }
}