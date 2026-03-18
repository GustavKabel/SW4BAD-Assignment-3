namespace AarhusSpaceProgram.Api.DTOs;

public class RocketDto
{
    public int RocketId { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public int PayloadCap { get; set; }
    public int CrewCap { get; set; }
    public int NoOfStages { get; set; }
    public int FuelCap { get; set; }
    public double Weight { get; set; }
}