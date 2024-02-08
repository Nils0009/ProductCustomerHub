namespace Infrastructure.Dtos;

public class OrderRegistrationDto
{
    public DateTime OrderDate { get; set; }
    public string CustomerNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PaymentMethodName { get; set; } = null!;
    public string Description { get; set; } = null!;
}
