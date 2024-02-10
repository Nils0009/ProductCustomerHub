using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class CategoryEntity
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
