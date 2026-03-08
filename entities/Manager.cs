using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.entities;

public class Manager
{
    [Required]
    [StringLength(100)]
    [Column("Department", TypeName = "nvarchar(100)")]
    public string? Department { get; set; }
}