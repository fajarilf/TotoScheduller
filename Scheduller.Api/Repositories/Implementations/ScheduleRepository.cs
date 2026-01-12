using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class ScheduleRepository : IRepository<Schedule>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Schedule> _dbSet;

        public ScheduleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Schedule>();
        }

        public DbSet<Schedule> DbSet => _dbSet;

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            return await _dbSet
                .Include(sc => sc.ScheduleDetails)
                .Include(sc => sc.Model)
                .ToListAsync();
        }

        public async Task<Schedule?> GetById(int id)
        {
            return await _dbSet
                .Include(sc => sc.ScheduleDetails)
                .Include(sc => sc.Model)
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<bool> Remove(Schedule entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<Schedule> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Schedule> SaveChanges(Schedule entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Schedule>> SaveRangeChages(IEnumerable<Schedule> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
    }
}
