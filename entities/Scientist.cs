using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.entities;

public class Scientist : Employee
{
    [Required]
    [StringLength(100)]
    [Column("Title", TypeName = "nvarchar(100)")]
    public string? Title { get; set; }
    [Required]
    [StringLength(100)]
    [Column("Speciality", TypeName = "nvarchar(100)")]
    public string? Speciality { get; set; }
}