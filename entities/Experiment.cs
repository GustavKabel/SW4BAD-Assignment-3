using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

public class Experiment
{
    [Key]
    [Column("experiment_id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Experiment name cannot exceed 100 characters.")]
    [Column("name", TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "Description name cannot exceed 100 characters.")]
    [Column("description", TypeName = "varchar(100)")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("creation_date")]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public int MissionId { get; set; }
    public Mission? Mission { get; set; }

    public int ScientistId { get; set; }
    public Scientist? Scientist { get; set; }
}