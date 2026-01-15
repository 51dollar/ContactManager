using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repository;

public abstract class BaseRepository<TEntity>(AppDbContext context) 
    where TEntity : class
{
    public virtual async Task<List<TEntity>> GetAllAsync() =>
        await context.Set<TEntity>().AsNoTracking().ToListAsync();
    
    public virtual async Task AddAsync(TEntity entity) =>
        await context.Set<TEntity>().AddAsync(entity);
    
    public virtual void Delete(TEntity entity) =>
        context.Set<TEntity>().Remove(entity);
    
    public virtual async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}