namespace AarhusSpaceProgram.Api.DTOs;

public class MissionLogDto
{
    public string? Id { get; set; }
    public int MissionId { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; }
}