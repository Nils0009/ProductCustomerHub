using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int PriceId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Price Price { get; set; } = null!;
}
