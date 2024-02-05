using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int AddressId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(10)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Country { get; set; } = null!;

    public virtual List<CustomerAddressEntity> CustomerAddresses { get; set; } = new List<CustomerAddressEntity>();
}
