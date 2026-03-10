using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("LaunchPad")]
public class LaunchPad
{
    [Key]
    [Column("launchpad_id")]
    public int LaunchPadId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
    [Column("location", TypeName = "nvarchar(100)")]
    public string? Location { get; set;}
    [Required]
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    [Column("status", TypeName = "nvarchar(50)")]
    public string Status { get; set;} = string.Empty;
    [Column("max_supported_weight")]
    public int MaxWeight { get; set; }

    public ICollection<Mission> Missions { get; set; } = new HashSet<Mission>();
}