using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class ModelRepository : IRepository<Model>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Model> _dbSet;

        public ModelRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Model>();
        }

        public DbSet<Model> DbSet => _dbSet;

        public async Task<Model> Update(Model entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Model>> GetAll()
        {
            return await _dbSet
                .Include(m => m.ProcessDetails)
                    .ThenInclude(pd => pd.Part)
                .ToListAsync();
        }

        public async Task<Model?> GetById(int id)
        {
            return await _dbSet
                .Include(m => m.ProcessDetails)
                    .ThenInclude(pd => pd.Part)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Model> SaveChanges(Model entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Model>> SaveRangeChages(IEnumerable<Model> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Remove(Model entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<Model> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
