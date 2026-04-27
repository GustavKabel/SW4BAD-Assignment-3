namespace AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Entities;
public class MissionDto
{
    public int MissionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly PlannedLaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    public MissionStatus Status { get; set; }
    public TypeOfMission Type { get; set; }

    public string ManagerName { get; set; } = string.Empty;
    public string RocketModel { get; set; } = string.Empty;
    public string LaunchPadLocation { get; set; } = string.Empty;
    public string TargetBodyName { get; set; } = string.Empty;
}