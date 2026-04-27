using System.ComponentModel.DataAnnotations;
namespace AarhusSpaceProgram.Api.DTOs;

public class RegisterDTO
{
    [Required]
    public string? FullName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}