using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

public class Manager: Employee
{
    [Required]
    [StringLength(100)]
    [Column("department", TypeName = "nvarchar(100)")]
    public string? Department { get; set; }
}