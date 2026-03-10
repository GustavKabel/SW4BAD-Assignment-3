using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("CelestialBody")]
public class CelestialBody
{
    [Key]
    [Column("body_id")]
    public int BodyId { get; set;}

    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exeec 100 characters.")]
    [Column("name", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }

    [Column("Distance")]
    public int Distance { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Body type cannot exceed 50 characters.")]
    [Column("body_type", TypeName = "nvarchar(50)")]
    public string? BodyType { get; set;}

    [Required]
    [StringLength(50, ErrorMessage = "Subtype cannot exceed 50 characters.")]
    [Column("subtype", TypeName = "nvarchar(50)")]
    public string? Subtype { get; set; }

    [Column("parent_body_id")]
    public int? ParentBodyId { get; set; }

    [ForeignKey(nameof(ParentBodyId))]
    public CelestialBody? ParentBody { get; set; }

    public ICollection<Mission> TargetMissions { get; set; } = new HashSet<Mission>();

}