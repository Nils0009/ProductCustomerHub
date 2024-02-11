namespace Infrastructure.Entities;

public partial class ProductEntity
{
    public string ArticleNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int PriceId { get; set; }

    public virtual CategoryEntity Category { get; set; } = null!;

    public virtual ManufacturerEntity Manufacturer { get; set; } = null!;

    public virtual PriceEntity Price { get; set; } = null!;
}
