using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Price
{
    public int Id { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
