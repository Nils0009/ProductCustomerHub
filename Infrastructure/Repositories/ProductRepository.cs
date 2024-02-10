using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<ProductEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _context;
    public ProductRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
