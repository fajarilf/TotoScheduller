using Scheduller.Api.Domains.DTOs;

namespace Scheduller.Api.Services.Interfaces
{
    public interface IScheduleDetailService
    {
        Task<IEnumerable<ScheduleDetailResponse>> GetAllScheduleDetail();
        Task<ScheduleDetailResponse> GetScheduleDetailById(int id);
        Task<IEnumerable<ScheduleDetailResponse>> CreateScheduleDetail(List<ScheduleDetailCreateRequest> request);
        Task<ScheduleDetailResponseWithModel> GetScheduleDetailByModelId(int model_id);
        Task<IEnumerable<ScheduleDetailResponseWithModel>> GetScheduleDetailByModel();
        Task<ScheduleDetailResponseWithModel> GetScheduleDetailByWorkCenterId (int work_center_id);
        Task<IEnumerable<ScheduleDetailResponseWithModel>> GetScheduleDetailByWorkCenter ();
        Task<bool> Delete(int id);
    }
}
