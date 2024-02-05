using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(PaymentMethodName), IsUnique = true)]
public class PaymentMethodEntity
{
    [Key]
    public int PaymentMethodId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string PaymentMethodName { get; set; } = null!;

    [Column(TypeName = "nvarchar(200)")]
    public string Description { get; set; } = null!;

    public virtual List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
