using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(RoleName), IsUnique = true)]
public class RoleEntity
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string RoleName { get; set; } = null!;

    public virtual List<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
}
