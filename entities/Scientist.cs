using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

public class Scientist : Employee
{
    [Required]
    [StringLength(100)]
    [Column("title", TypeName = "nvarchar(100)")]
    public string? Title { get; set; }
    [Required]
    [StringLength(100)]
    [Column("speciality", TypeName = "nvarchar(100)")]
    public string? Speciality { get; set; }
}