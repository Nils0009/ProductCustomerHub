using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]
    public int OrderNumber { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [ForeignKey(nameof(CustomerEntity))]
    public int CustomerNumber { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
