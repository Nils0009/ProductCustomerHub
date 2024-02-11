using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ProductRepository(ProductCatalogContext context) : GenericRepository<ProductEntity, ProductCatalogContext>(context)
{
    private readonly ProductCatalogContext _context = context;
}
