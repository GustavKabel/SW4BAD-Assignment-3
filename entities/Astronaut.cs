using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("Astronaut")]
public class Astronaut : Employee
{
    [Required]
    [StringLength(50, ErrorMessage = "Paygrade cannot exceed 50 characters.")]
    [Column("paygrade", TypeName = "nvarchar(50)")]
    public string Paygrade { get; set; } = string.Empty;

    [Required]
    [StringLength(50, ErrorMessage = "Rank cannot exceed 50 characters.")]
    [Column("rank", TypeName = "nvarchar(50)")]
    public string Rank { get; set; } = string.Empty;

    [Column("hours_in_space")]
    public int HoursInSpace { get; set; }

    [Column("hours_in_sim")]
    public int HoursInSimulation { get; set; }

    public ICollection<Mission> Missions { get; set; } = new HashSet<Mission>();

}