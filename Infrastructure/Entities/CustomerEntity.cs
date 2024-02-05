using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public string CustomerNumber { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Email { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;
    public virtual List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    public virtual List<CustomerAddressEntity> CustomerAddresses { get; set; } = new List<CustomerAddressEntity>();
}
