using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AarhusSpaceProgram.Api.Entities;


public class AppUser : IdentityUser
{
    [MaxLength(100)]
    public string? FullName { get; set; }
}

