namespace Infrastructure.Dtos;

public class OrderDto
{
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Description { get; set; } = null!;
    public string CustomerNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PaymentMethodName { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}
