using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class PaymentMethodRepository(CustomerManagementContext context) : GenericRepository<PaymentMethodEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
}
