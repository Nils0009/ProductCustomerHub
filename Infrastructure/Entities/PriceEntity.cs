using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class PriceEntity
{
    public int Id { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
