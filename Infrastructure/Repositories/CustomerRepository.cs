using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerManagementContext context) : GenericRepository<CustomerEntity, CustomerManagementContext>(context)
{
    private readonly CustomerManagementContext _context = context;

    public override IEnumerable<CustomerEntity> GetAll()
    {
        try
        {
            var allEntities = _context.Customers
                .Include(x => x.Role)
                    .ThenInclude(x => x.RoleName)
                    .ToList();
            if (allEntities != null)
            {
                return allEntities;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public override CustomerEntity GetOne(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = _context.Customers
                .Include (x => x.Role)
                    .ThenInclude (x => x.RoleName)
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
