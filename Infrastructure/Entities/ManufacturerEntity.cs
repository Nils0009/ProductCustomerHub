namespace Infrastructure.Entities;

public partial class ManufacturerEntity
{
    public int Id { get; set; }

    public string Manufacturer { get; set; } = null!;

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
