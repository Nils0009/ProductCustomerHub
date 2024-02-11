using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class PriceRepository(ProductCatalogContext context) : GenericRepository<PriceEntity, ProductCatalogContext>(context)
{
    private readonly ProductCatalogContext _context = context;
}
