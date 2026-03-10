using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

public class Astronaut : Employee
{
    [Required]
    public int Paygrade { get; set; }
    [Required]
    [StringLength(50)]
    [Column("rank", TypeName = "nvarchar(50)")]
    public string? Rank { get; set; }    
    public int HoursInSpace { get; set; }
    public int HoursInSimulation { get; set; }

}