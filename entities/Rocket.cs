using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AarhusSpaceProgram.Api.Entities;

public class Rocket
{
    [Key]
    public int RocketId { get; set; }
    [Required]
    [StringLength(100)]
    [Column("model_name", TypeName = "nvarchar(100)")]
    public string? ModelName { get; set; }
    // Weight cannot be negative, which we will enforce in the DbContext
    public int PayloadCap { get; set; }
    public int CrewCap { get; set; }
    public int NoOfStages { get; set; }
    public int FuelCap { get; set; }
    public double Weight { get; set; } 
    
}
