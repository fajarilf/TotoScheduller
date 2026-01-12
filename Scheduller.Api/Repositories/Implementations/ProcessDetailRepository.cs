using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class ProcessDetailRepository : IRepository<ProcessDetail>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ProcessDetail> _dbSet;

        public ProcessDetailRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProcessDetail>();
        }

        public DbSet<ProcessDetail> DbSet => _dbSet;

        public async Task<IEnumerable<ProcessDetail>> GetAll()
        {
            return await _dbSet
                .Include(pd => pd.Model)
                .Include(pd => pd.Part)
                .ToListAsync();
        }

        public async Task<ProcessDetail?> GetById(int id)
        {
            return await _dbSet
                .Include(pd => pd.Model)
                .Include(pd => pd.Part)
                .FirstOrDefaultAsync(pd => pd.Id == id);
        }

        public async Task<ProcessDetail> SaveChanges(ProcessDetail entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ProcessDetail>> SaveRangeChages(IEnumerable<ProcessDetail> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Remove(ProcessDetail entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<ProcessDetail> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
