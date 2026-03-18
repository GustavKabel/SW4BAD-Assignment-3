namespace AarhusSpaceProgram.Api.DTOs;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly HireDate { get; set; }
}