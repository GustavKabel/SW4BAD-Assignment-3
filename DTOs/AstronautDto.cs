namespace AarhusSpaceProgram.Api.DTOs;

public class AstronautDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Rank { get; set; } = string.Empty;
    public string Paygrade { get; set; } = string.Empty;
    public int HoursInSpace { get; set; }
    public int HoursInSimulation { get; set; } 
}