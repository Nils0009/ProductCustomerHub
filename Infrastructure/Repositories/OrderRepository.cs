using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OrderRepository(CustomerManagementContext context) : GenericRepository<OrderEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;

    public override async Task<OrderEntity> GetOne(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = await _context.Orders
                .Include(x => x.Customer)
                    .ThenInclude(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                .FirstOrDefaultAsync(predicate);

            return entityToFind!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
}
