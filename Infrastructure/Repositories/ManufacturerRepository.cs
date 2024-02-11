using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ManufacturerRepository(ProductCatalogContext context) : GenericRepository<ManufacturerEntity, ProductCatalogContext>(context)
{
    private readonly ProductCatalogContext _context = context;
}
