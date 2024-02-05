using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerAddressRepository(CustomerManagementContext context) : GenericRepository<CustomerAddressEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
}
