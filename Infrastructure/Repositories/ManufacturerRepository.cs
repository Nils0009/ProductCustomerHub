using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ManufacturerRepository : GenericRepository<ManufacturerEntity, ProductCatalogContext>
{
    private readonly ProductCatalogContext _context;
    public ManufacturerRepository(ProductCatalogContext context) : base(context)
    {
        _context = context;
    }
}
