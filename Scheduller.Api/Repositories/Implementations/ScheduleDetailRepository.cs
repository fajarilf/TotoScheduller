using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;
using Scheduller.Api.Infrasturctures;
using Scheduller.Api.Repositories.Interfaces;

namespace Scheduller.Api.Repositories.Implementations
{
    public class ScheduleDetailRepository : IRepository<ScheduleDetail>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<ScheduleDetail> _dbSet;

        public ScheduleDetailRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ScheduleDetail>();
        }

        public DbSet<ScheduleDetail> DbSet => _dbSet;

        public async Task<IEnumerable<ScheduleDetail>> GetAll()
        {
            return await _dbSet
                .Include(sd => sd.Part)
                .Include(sd => sd.WorkCenter)
                .ToListAsync();
        }

        public async Task<ScheduleDetail?> GetById(int id)
        {
            return await _dbSet
                .Include(sd => sd.Part)
                .Include(sd => sd.WorkCenter)
                .FirstOrDefaultAsync(sd => sd.Id == id);
        }

        public async Task<bool> Remove(ScheduleDetail entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(IEnumerable<ScheduleDetail> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ScheduleDetail> SaveChanges(ScheduleDetail entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ScheduleDetail>> SaveRangeChages(IEnumerable<ScheduleDetail> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
    }
}
