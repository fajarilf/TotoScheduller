using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class ProcessComponentRepository : IRepository<ProcessComponent>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ProcessComponent> _dbSet;

        public ProcessComponentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProcessComponent>();
        }

        public DbSet<ProcessComponent> DbSet => _dbSet;

        public async Task<IEnumerable<ProcessComponent>> GetAll()
        {
            return await _dbSet
                .Include(pc => pc.Part)
                .Include(pc => pc.WorkCenter)
                .ToListAsync();
        }

        public async Task<ProcessComponent?> GetById(int id)
        {
            return await _dbSet
                .Include(pc => pc.Part)
                .Include(pc => pc.WorkCenter)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProcessComponent> SaveChanges(ProcessComponent entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ProcessComponent>> SaveRangeChages(IEnumerable<ProcessComponent> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Remove(ProcessComponent entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<ProcessComponent> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
