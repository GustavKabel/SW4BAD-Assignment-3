using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.Api.Entities;
[Table("Scientist")]
public class Scientist : Employee
{
    [Required]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    [Column("title", TypeName = "nvarchar(100)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "Speciality cannot exeec 100 characters.")]
    [Column("speciality", TypeName = "nvarchar(100)")]
    public string Speciality { get; set; } = string.Empty;

    public ICollection<Mission> Missions { get; set; } = new HashSet<Mission>();

    public ICollection<Experiment> Experiments { get; set; } = new HashSet<Experiment>();
}