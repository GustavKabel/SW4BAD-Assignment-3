using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.entities;

public class CelestialBody
{
    [Key]
    public int BodyId { get; set;}
    [Required]
    [StringLength(100)]
    [Column("name", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    public int Distance { get; set; }
    [Required]
    [StringLength(50)]
    [Column("body_type", TypeName = "nvarchar(50)")]
    public string? BodyType { get; set;}
    [Required]
    [StringLength(50)]
    [Column("subtype", TypeName = "nvarchar(50)")]
    public string? Subtype { get; set; }
    public int? ParentBodyId { get; set; }
    [ForeignKey(nameof(ParentBodyId))]
    public CelestialBody? ParentBody { get; set; }

}