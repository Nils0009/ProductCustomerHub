using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class GenericRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;
    protected GenericRepository(TContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        try
        {
            var allEntities = await _context.Set<TEntity>().ToListAsync();
            return allEntities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public virtual async Task<TEntity> GetOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return entityToFind!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null;
    }

    public virtual async Task<TEntity> Update(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        try
        {
            var entityToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return entityToUpdate;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public virtual async Task<bool> Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityToRemove = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (entityToRemove != null)
            {
                _context.Set<TEntity>().Remove(entityToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().AnyAsync(predicate);
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
