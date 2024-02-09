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

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual IEnumerable<TEntity> GetAll()
    {
        try
        {
            var allEntities = _context.Set<TEntity>().ToList();
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
    public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityToFind = _context.Set<TEntity>().FirstOrDefault(predicate);
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
    public virtual TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(predicate);
            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);
                _context.Update(entityToUpdate);
                _context.SaveChanges();
                return entityToUpdate;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityToRemove = _context.Set<TEntity>().FirstOrDefault(predicate);
            if (entityToRemove != null)
            {
                _context.Set<TEntity>().Remove(entityToRemove);
                _context.SaveChanges();
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var existingEntity = _context.Set<TEntity>().Any(predicate);
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
