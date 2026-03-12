namespace AarhusSpaceProgram.Api.DTOs;

public class MissionDto
{
    public int MissionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly PlannedLaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public string ManagerName { get; set; } = string.Empty;
    public string RocketModel { get; set; } = string.Empty;
    public string LaunchPadLocation { get; set; } = string.Empty;
    public string TargetBodyName { get; set; } = string.Empty;
    public List<AstronautDto> Crew { get; set; } = new();
}