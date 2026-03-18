namespace AarhusSpaceProgram.Api.DTOs;

public class LaunchPadDto
{
    public int LaunchPadId { get; set; }
    public string? Location { get; set; }
    public string Status { get; set; } = string.Empty;
    public double MaxWeight { get; set; }
}