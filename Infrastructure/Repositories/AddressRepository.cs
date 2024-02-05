using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressRepository(CustomerManagementContext context) : GenericRepository<AddressEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
}
