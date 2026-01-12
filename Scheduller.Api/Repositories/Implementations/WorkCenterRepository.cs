using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class WorkCenterRepository : IRepository<WorkCenter>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<WorkCenter> _dbSet;

        public WorkCenterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<WorkCenter>();
        }

        public DbSet<WorkCenter> DbSet => _dbSet;

        public async Task<IEnumerable<WorkCenter>> GetAll()
        {
            return await _dbSet
                .Include(wc => wc.ProcessComponents)
                    .ThenInclude(pc => pc.Part)
                .ToListAsync();
        }

        public async Task<WorkCenter?> GetById(int id)
        {
            return await _dbSet
                .Include(wc => wc.ProcessComponents)
                    .ThenInclude(pc => pc.Part)
                .FirstOrDefaultAsync(wc => wc.Id == id);
        }

        public async Task<WorkCenter> SaveChanges(WorkCenter entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<WorkCenter>> SaveRangeChages(IEnumerable<WorkCenter> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Remove(WorkCenter entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<WorkCenter> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
