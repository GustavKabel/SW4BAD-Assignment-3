using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.entities;

public class LaunchPad
{
    [Key]
    public int LaunchPadId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("location", TypeName = "nvarchar(100)")]
    public string? Location { get; set;}
    [Required]
    [StringLength(50)]
    [Column("status", TypeName = "nvarchar(50)")]
    public string? Status { get; set;} // May need to be enum instead
    public int MaxWeight { get; set; }
}