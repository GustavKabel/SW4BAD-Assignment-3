using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.entities;

public class Employee
{
    [Key]
    public int Employee_id { get; set; }
    [Required]
    [StringLength(100)]
    [Column("FullName", TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    public DateOnly Hire_date { get; set; }
}