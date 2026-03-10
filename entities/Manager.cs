using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;

[Table("Manager")]
public class Manager: Employee
{
    [Required]
    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
    [Column("department", TypeName = "nvarchar(100)")]
    public string Department { get; set; } = string.Empty;

    public ICollection<Mission> ManagedMissions { get; set; } = new HashSet<Mission>();
}