using Microsoft.EntityFrameworkCore;

namespace Scheduller.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<T> SaveChanges(T entity);
        Task<IEnumerable<T>> SaveRangeChages (IEnumerable<T> entities);
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Remove(T entity);
        Task<bool> RemoveRange(IEnumerable<T> entities);
    }
}
