using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AarhusSpaceProgram.Api.Entities;
[Table("Rocket")]
public class Rocket
{
    [Key]
    [Column("rocket_id")]
    public int RocketId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Model name cannot exceed 100 characters")]
    [Column("model_name", TypeName = "nvarchar(100)")]
    public string ModelName { get; set; } = string.Empty;
    // Weight cannot be negative, which we will enforce in the DbContext
    [Column("payload_cap")]
    public int PayloadCap { get; set; }
    [Column("crew_cap")]
    public int CrewCap { get; set; }
    [Column("no_of_stages")]
    public int NoOfStages { get; set; }
    [Column("fuel_cap")]
    public int FuelCap { get; set; }
    [Column("weight")]
    public double Weight { get; set; } 

    public Mission? Mission { get; set; }
    
}
