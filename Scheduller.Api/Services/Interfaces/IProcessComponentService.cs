using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IProcessComponentService
    {
        Task<IEnumerable<ProcessComponentResponseRelation>> GetAllProcessComponent();
        Task<ProcessComponentResponseRelation> GetProcessComponentById(int id);
        Task<IEnumerable<ProcessComponentResponseRelation>> GetAllProcessComponentByPartId(int part_id);
        Task<bool> Delete (int id);
    }
}
