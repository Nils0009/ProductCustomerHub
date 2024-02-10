using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<CategoryEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _context;
    public CategoryRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
