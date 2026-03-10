using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("Mission")]
public class Mission
{
    [Key]
    [Column("mission_id")]
    public int MissionId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Mission name cannot exceed 100 characters.")]
    [Column("mission_id", TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Column("planned_launch_date")]
    public DateOnly PlannedLaunchDate { get; set; }

    [Column("planned_duration")]
    public int PlannedDuration { get; set; }
    // we let status be a string as we use enums later
    [Required]
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    [Column("status", TypeName = "varchar(50)")]
    public string Status { get; set; } = string.Empty;
    [Required]
    [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
    [Column("type", TypeName = "varchar(50)")]
    public string Type { get; set; } = string.Empty;

    // Foerign Keys

    [Column("manager_id")]
    public int ManagerId { get; set; }

    [ForeignKey(nameof(ManagerId))]
    public Manager Manager { get; set; } = null!;

    [Column("rocket_id")]
    public int RocketId { get; set; }

    [ForeignKey(nameof(RocketId))]
    public Rocket Rocket { get; set; } = null!;

    [Column("launchpad_id")]
    public int LaunchPadId { get; set;}

    [ForeignKey(nameof(LaunchPadId))]
    public LaunchPad LaunchPad { get; set;} = null!;
    
    [Column("target_body_id")]
    public int TargetBodyId { get; set; }
    [ForeignKey(nameof(TargetBodyId))]
    public CelestialBody TargetBody { get; set; } = null!;

    public ICollection<Astronaut> Astronauts { get; set; } = new HashSet<Astronaut>();
    public ICollection<Scientist> Scientists { get; set; } = new HashSet<Scientist>(); 
}