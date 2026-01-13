using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IProcessDetailService
    {
        Task<IEnumerable<ProcessDetailResponseRelation>> GetAllProcessDetail();
        Task<ProcessDetailResponseRelation> GetProcessDetailById(int id);
        Task<IEnumerable<ProcessDetailResponseRelation>> GetAllProcessDetailByModelId(int model_id);
    }
}
