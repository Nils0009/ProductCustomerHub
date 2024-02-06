using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerManagementContext context) : GenericRepository<CustomerEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;
    public override CustomerEntity GetOne(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = _context.Customers
                .Include (x => x.Role)
                .Include(x => x.CustomerAddresses)
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
