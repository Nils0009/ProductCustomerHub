using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository(ProductCatalogContext context) : GenericRepository<CategoryEntity, ProductCatalogContext>(context)
{
    private readonly ProductCatalogContext _context = context;
}
