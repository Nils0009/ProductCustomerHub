using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RoleRepository(CustomerManagementContext context) : GenericRepository<RoleEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
}