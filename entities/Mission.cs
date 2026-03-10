using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.entities;

public class Mission
{
    [Key]
    public int MissionId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("mission_id", TypeName = "varchar(100)")]
    public string? Name { get; set; }
    public DateOnly PlannedLaunchDate { get; set; }
    public int PlannedDuration { get; set; }
    // we let status be a string as we use enums later
    [Required]
    [StringLength(50)]
    [Column("status", TypeName = "varchar(50)")]
    public string? Status { get; set; }
    [Required]
    [StringLength(50)]
    [Column("type", TypeName = "varchar(50)")]
    public string? Type { get; set; }
    public int ManagerId { get; set; }
    [ForeignKey(nameof(ManagerId))]
    public Manager Manager { get; set; } = null!;
    public int RocketId { get; set; }
    [ForeignKey(nameof(RocketId))]
    public Rocket Rocket { get; set; } = null!;
    public int LaunchPadId { get; set;}
    [ForeignKey(nameof(LaunchPadId))]
    public LaunchPad LaunchPad { get; set;} = null!;
    public int TargetBodyId { get; set; }
    [ForeignKey(nameof(TargetBodyId))]
    public CelestialBody TargetBody { get; set; } = null!;
}