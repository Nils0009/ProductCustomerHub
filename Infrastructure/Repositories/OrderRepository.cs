using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class OrderRepository(CustomerManagementContext context) : GenericRepository<OrderEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
}
