using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerAddressEntity
{
    [ForeignKey(nameof(Customer))]
    public string CustomerNumber { get; set; } = null!;
    public CustomerEntity Customer { get; set; } = null!;

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;
}
