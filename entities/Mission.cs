using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

public enum MissionStatus
{
    Created,
    Budgeted,
    Approved,
    Planned,
    Active,
    Completed,
    Aborted,
    Failed
}

public enum TypeOfMission
{
    None,
    Orbiting,
    Landing
}

[Table("Mission")]
public class Mission
{
    //Mission id
    [Key]
    [Column("mission_id")]
    public int MissionId { get; set; }

    //name
    [Required]
    [StringLength(100, ErrorMessage = "Mission name cannot exceed 100 characters.")]
    [Column("name", TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    //Planned launch data
    [Column("planned_launch_date")]
    public DateOnly PlannedLaunchDate { get; set; }

    //Planned duration
    [Column("planned_duration")]
    public int PlannedDuration { get; set; }

    //Status
    [Required]
    [Column("status", TypeName = "varchar(50)")]
    public MissionStatus Status { get; set; } = MissionStatus.Created;

    public void UpdateStatus(MissionStatus newStatus)
    {
        // A mission cannot directly go from created to active
        if (Status == MissionStatus.Created && newStatus == MissionStatus.Active)
        {
            throw new InvalidOperationException("A mission cannot move directly from created to active");
        }
        // A mission completed cannot become active again
        if (Status == MissionStatus.Completed && newStatus == MissionStatus.Active)
        {
            throw new InvalidOperationException("A mission cannot move completed to active");
        }
        // Only active missions can move to Completed, failed or aborted
        if ((newStatus == MissionStatus.Completed || newStatus == MissionStatus.Failed || newStatus == MissionStatus.Aborted) && Status != MissionStatus.Active)
        {
            throw new InvalidOperationException("Only active missions can move to completed, failed or aborted");
        }
        // Ensure that at least one astronauts have been assigned to a mission before launch
        if (newStatus == MissionStatus.Active)
        {
            if (Astronauts.Count == 0)
            {
                throw new InvalidOperationException("A mission cannot launch without atleast one assigned Astronaut");
            }
        }

        Status = newStatus;
    }

    //Type of mission
    [Required]
    [Column("type", TypeName = "varchar(50)")]
    public TypeOfMission Type { get; set; } = TypeOfMission.None;

    // Foreign Keys
    
    [Column("manager_id")]
    public int? ManagerId { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public Manager? Manager { get; set; } = null!;

    [Column("rocket_id")]
    public int RocketId { get; set; }

    public Rocket Rocket { get; set; } = null!;

    [Column("launchpad_id")]
    public int LaunchPadId { get; set; }

    [ForeignKey(nameof(LaunchPadId))]
    public LaunchPad LaunchPad { get; set; } = null!;

    [Column("target_body_id")]
    public int TargetBodyId { get; set; }
    [ForeignKey(nameof(TargetBodyId))]
    public CelestialBody TargetBody { get; set; } = null!;

    public ICollection<Astronaut> Astronauts { get; set; } = new HashSet<Astronaut>();
    public ICollection<Scientist> Scientists { get; set; } = new HashSet<Scientist>();
}