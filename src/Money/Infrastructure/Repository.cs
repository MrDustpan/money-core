using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Money.Infrastructure
{
  public class Repository<T> : IRepository<T> where T : AggregateRoot
  {
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
      return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<T>> ListAsync()
    {
      return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
      if (entity.Id == Guid.Empty)
      {
        entity.Id = Guid.NewGuid();
      }
      _dbContext.Set<T>().Add(entity);
      await _dbContext.SaveChangesAsync();

      return entity;
    }

    public async Task DeleteAsync(T entity)
    {
      _dbContext.Set<T>().Remove(entity);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
      _dbContext.Entry(entity).State = EntityState.Modified;
      await _dbContext.SaveChangesAsync();
    }
  }

  public interface IRepository<T> where T : AggregateRoot
  {
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> ListAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
  }
}