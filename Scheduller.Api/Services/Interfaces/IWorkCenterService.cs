using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IWorkCenterService
    {
        Task<IEnumerable<WorkCenterResponseRelation>> GetAllWorkCenter();
        Task<WorkCenterResponseRelation?> GetWorkCenterById(int id);
        Task<bool> Delete(int id);
        Task<WorkCenterResponse> CreateWorkCenter(WorkCenterCreateRequest request);
        Task<WorkCenterResponse> UpdateWorkCenter(WorkCenterUpdateRequest request);
    }
}
