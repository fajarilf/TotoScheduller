using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class PartRepository : IRepository<Part>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Part> _dbSet;

        public PartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Part>();
        }

        public DbSet<Part> DbSet => _dbSet;

        public async Task<IEnumerable<Part>> GetAll()
        {
            return await _dbSet
                .Include(p => p.ProcessDetails)
                    .ThenInclude(pd => pd.Model)
                .ToListAsync();
        }

        public async Task<Part?> GetById(int id)
        {
            return await _dbSet
                .Include(p => p.ProcessDetails)
                .Include(p => p.ProcessComponents)
                    .ThenInclude(pc => pc.WorkCenter)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Part> SaveChanges(Part entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Part>> SaveRangeChages(IEnumerable<Part> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Remove(Part entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<Part> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
