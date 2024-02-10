using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class PriceRepository : GenericRepository<PriceEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _context;
    public PriceRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
