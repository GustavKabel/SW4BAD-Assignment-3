using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;


public enum CelestialBodyType
{
    None,
    Planet,
    Moon
}

public enum CelestialBodySubType
{
    None,
    RockyPlanet,
    GasGiant
}

[Table("CelestialBody")]
public class CelestialBody
{
    [Key]
    [Column("body_id")]
    public int BodyId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exeec 100 characters.")]
    [Column("name", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }

    [Column("Distance")]
    public int Distance { get; set; }

    [Required]
    [Column("body_type", TypeName = "nvarchar(50)")]
    public CelestialBodyType BodyType { get; set; } = CelestialBodyType.None;

    [Required]
    [Column("subtype", TypeName = "nvarchar(50)")]
    public CelestialBodySubType Subtype { get; set; } = CelestialBodySubType.None;

    [Column("parent_body_id")]
    public int? ParentBodyId { get; set; }

    [ForeignKey(nameof(ParentBodyId))]
    public CelestialBody? ParentBody { get; set; }

    public ICollection<Mission> TargetMissions { get; set; } = new HashSet<Mission>();

}