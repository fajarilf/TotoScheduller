using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IPartService
    {
        Task<IEnumerable<PartResponseRelation>> GetAllPart();
        Task<PartResponseRelation> GetPartById(int id);
        Task<bool> Delete(int id);
    }
}
