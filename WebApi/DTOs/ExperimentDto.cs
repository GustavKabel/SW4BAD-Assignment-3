namespace AarhusSpaceProgram.Api.DTOs;
public class ExperimentDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public int MissionId { get; set; }
    public int ScientistId { get; set; }
}