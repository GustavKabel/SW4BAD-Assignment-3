using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.entities;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [Required]
    [StringLength(100)]
    [Column("name", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    public DateOnly HireDate { get; set; }
}