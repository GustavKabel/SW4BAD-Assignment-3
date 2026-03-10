using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("Employee")]
public abstract class Employee
{
    [Key]
    [Column("employee_id")]
    public int EmployeeId { get; set; }
    
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot be more than 100 characters.")]
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Column("hire_date")]
    public DateOnly HireDate { get; set; }
}