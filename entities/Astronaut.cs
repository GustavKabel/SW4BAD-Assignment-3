using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.entities;

public class Astronaut : Employee
{
    public int Paygrade { get; set; }
    [Required]
    [StringLength(50)]
    [Column("Rank", TypeName = "nvarchar(50)")]
    public string? Rank { get; set; }    
    public int Hours_in_space { get; set; }
    public int Hours_in_simulation { get; set; }

}