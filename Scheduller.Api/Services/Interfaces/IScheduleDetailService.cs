using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IScheduleDetailService
    {
        Task<PagedResult<ScheduleDetailResponseTable>> GetAllScheduleDetail(DateTime? date, int? model_id, int page, int pageSize);
        Task<ScheduleDetailResponse> GetScheduleDetailById(int id);
        Task<IEnumerable<ScheduleDetailResponse>> CreateScheduleDetail(List<ScheduleDetailCreateRequest> request);
        Task<ScheduleDetailResponseWithModel> GetScheduleDetailByModelId(int model_id);
        Task<IEnumerable<ScheduleDetailResponseWithModel>> GetScheduleDetailByModel();
        Task<ScheduleDetailResponseWithWorkCenter> GetScheduleDetailByWorkCenterId (int work_center_id);
        Task<IEnumerable<ScheduleDetailResponseWithWorkCenter>> GetScheduleDetailByWorkCenter ();
        Task<bool> Delete(int id);
    }
}
