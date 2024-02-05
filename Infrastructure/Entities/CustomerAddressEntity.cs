namespace Infrastructure.Entities;

public class CustomerAddressEntity
{
    public int CustomerNumber { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;

    public int AddressId { get; set; }
    public virtual AddressEntity Address { get; set; } = null!;
}
