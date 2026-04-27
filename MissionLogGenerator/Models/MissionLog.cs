namespace MissionLogGenerator.Models;

public class MissionLog
{
    public Guid id { get; set; }
    public int MissionId { get; set; }
    public string? Message { get; set; }
    public DateTime TimeStamp { get; set; }
}