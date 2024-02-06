namespace Infrastructure.Dtos;

public class CustomerDto
{
    public string CustomerNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}
