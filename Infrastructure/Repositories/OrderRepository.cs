using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OrderRepository(CustomerManagementContext context) : GenericRepository<OrderEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;

    public override OrderEntity GetOne(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = _context.Orders
                .Include(x => x.Customer)
                    .ThenInclude(x => x.CustomerAddresses)
                    .ThenInclude(x => x.Address)
                .FirstOrDefault(predicate);

            if (entityToFind != null)
            {
                return entityToFind;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
}
